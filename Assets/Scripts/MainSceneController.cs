using Globals;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{

    public GameObject rulesPanel;
    public GameObject allPlayersPanel;
    public GameObject singlePlayerPanel;
    public GameObject pauzePanel;
    public GameObject toEnteringFirstWordPanel;
    public GameObject toShowingFirstWordsPanel;
    public GameObject toEnteringSecondWordPanel;
    public GameObject toShowingSecondWordsPanel;
    public GameObject toEnteringOutsiderPanel;
    public GameObject toShowingOutsiderPanel;
    public GameObject endOverviewRestartPanel;
    public GameObject settingsPanel;
    public GameObject settingsButton;

    private AllPlayersPanelController apc;
    private SinglePlayerPanelController spc;

    // Start is called before the first frame update
    void Start()
    {
        //team1Score = PlayerPrefs.GetInt("Team1Score");
        apc = allPlayersPanel.GetComponent<AllPlayersPanelController>();
        spc = singlePlayerPanel.GetComponent<SinglePlayerPanelController>();
        GameManager.Initialize();
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
        // Initialize localization first
        LocalizationSettings.InitializationOperation.WaitForCompletion();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(SettingsPanelController.GetLocale());

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
    }

    private void OnDestroy()
    {
        WordSetGenerator.BuzzWordsGenerated -= OnBuzzWordsGenerated;
    }

    // called by buttons, don't refactor away
    public void NextState()
    {
        GameManager.NextState();
    }

    public void OnAllPlayersReady()
    {
        if (GameManager.GetState() == State.EnteringPlayers)
        {
            if (apc.ValidateInput())
            {

                GameManager.SetPlayers(apc.GetPlayers());
                GameManager.NextState();
                switch (GameManager.GetGeneratorType())
                {
                    case GeneratorType.OpenAI:
                        Debug.Log("calling openai async");
                        StartCoroutine(WordSetGenerator.GenerateBuzzWordsAsync());
                        Debug.Log("called openai async");
                        break;
                    case GeneratorType.Claude:
                        Debug.Log("calling claude async");
                        StartCoroutine(WordSetGenerator.GenerateBuzzWordsAsync());
                        Debug.Log("called claude async");
                        break;
                    default:
                        Debug.Log("hardcoded sync");
                        WordSetGenerator.GenerateBuzWordsSync(WordSetGenerator.GetOriginStaticListText());
                        break;
                }
            }
        }
        else
        {
            NextState();
        }
    }

    private void ActivatePanels()
    {
        rulesPanel.SetActive(GameManager.GetState() == State.Rules);
        pauzePanel.SetActive(GameManager.GetState() == State.GeneratingWords);
        allPlayersPanel.SetActive(GameManager.GetState() == State.EnteringPlayers
                                || GameManager.GetState() == State.ShowingFirstWords
                                || GameManager.GetState() == State.ShowingSecondWords
                                || GameManager.GetState() == State.ShowingOutsider);
        singlePlayerPanel.SetActive(GameManager.GetState() == State.ShowChosenWord
                                || GameManager.GetState() == State.EnteringFirstWord
                                || GameManager.GetState() == State.EnteringSecondWord
                                || GameManager.GetState() == State.EnteringOutsider);
        toEnteringFirstWordPanel.SetActive(GameManager.GetState() == State.ToEnteringFirstWord);
        toShowingFirstWordsPanel.SetActive(GameManager.GetState() == State.ToShowingFirstWords);
        toEnteringSecondWordPanel.SetActive(GameManager.GetState() == State.ToEnteringSecondWord);
        toShowingSecondWordsPanel.SetActive(GameManager.GetState() == State.ToShowingSecondWords);
        toEnteringOutsiderPanel.SetActive(GameManager.GetState() == State.ToEnteringOutsider);
        toShowingOutsiderPanel.SetActive(GameManager.GetState() == State.ToShowingOutsider);
        endOverviewRestartPanel.SetActive(GameManager.GetState() == State.EndOverviewRestart);
        settingsButton.SetActive(GameManager.GetState() != State.Rules);
    }

    public void OnSettings()
    {
        settingsPanel.SetActive(true);
        settingsButton.SetActive(false);
    }

    public void OnExit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

}
