using Globals;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{

    public GameObject panelRules;
    public GameObject panelAllPlayers;
    public GameObject panelSinglePlayer;
    public GameObject panelPauze;

    private AllPlayersPanelController apc;
    private SinglePlayerPanelController spc;

    // Start is called before the first frame update
    void Start()
    {
        //team1Score = PlayerPrefs.GetInt("Team1Score");
        apc = panelAllPlayers.GetComponent<AllPlayersPanelController>();
        spc = panelSinglePlayer.GetComponent<SinglePlayerPanelController>();
        GameManager.SetState(State.Rules);
        ActivatePanels();

        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaObject act = new AndroidJavaClass("com.unity3d.player.UnityPlayer") // Get the Unity Player.
                .GetStatic<AndroidJavaObject>("currentActivity"); // Get the Current Activity from the Unity Player.
            act.Call("setShowWhenLocked", true);
        }

    }

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        WordSetGenerator.BuzzWordsGenerated += OnBuzzWordsGenerated;
        GameManager.NextStateEvent += GameManager_NextStateEvent;
    }

    private void GameManager_NextStateEvent()
    {
        ActivatePanels();
    }

    private void OnBuzzWordsGenerated()
    {
        GameManager.NextState();
        spc.NextPlayer();
    }

    private void OnDestroy()
    {
        WordSetGenerator.BuzzWordsGenerated -= OnBuzzWordsGenerated;
    }
    public void OnRulesOK()
    {
        GameManager.NextState();
    }

    public void OnAllPlayersReady()
    {
        if (GameManager.GetState() == State.EnteringPlayers)
        {
            GameManager.SetPlayers(apc.GetPlayers());
            GameManager.NextState();
            switch (GameManager.generatorType)
            {
                case GeneratorType.OpenAI:
                    WordSetGenerator.GenerateBuzzWordsAsync();
                    break;
                default:
                    WordSetGenerator.GenerateBuzWordsSync();
                    break;
            }
        }
    }

    private void ActivatePanels()
    {
        panelRules.SetActive(GameManager.GetState() == State.Rules);
        panelPauze.SetActive(GameManager.GetState() == State.Pauze);
        panelAllPlayers.SetActive(GameManager.GetState() == State.EnteringPlayers
                                || GameManager.GetState() == State.ShowingFirstWords
                                || GameManager.GetState() == State.ShowingSecondWords
                                || GameManager.GetState() == State.ShowingOutsider);
        panelSinglePlayer.SetActive(GameManager.GetState() == State.ShowChosenWord
                                || GameManager.GetState() == State.EnteringFirstWord
                                || GameManager.GetState() == State.EnteringSecondWord
                                || GameManager.GetState() == State.EnteringOutsider);
    }

    public void OnLayout()
    {
        PlayerPrefs.SetString("Layout", "something");
        PlayerPrefs.Save();
    }

    private void SaveScores() { 
        PlayerPrefs.SetInt("Team1Score", 1);
        PlayerPrefs.SetInt("Team2Score", 2);
        PlayerPrefs.Save();
    }

    public void OnExit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

}
