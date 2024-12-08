using Globals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllPlayersPanelController : MonoBehaviour
{

    public GameObject contentPanel;
    public GameObject playerInfoPrefab;
    public GameObject readyButton;
    public GameObject errorMessage;

    private float contentPanelHeigth = 0f;
    private float spacing = 15f;
    private List<GameObject> playerInfoObjects = new List<GameObject>();
    private int maxPlayers = 10;

    void Awake()
    {
        GameManager.NextStateEvent += GameManager_NextStateEvent;
    }

    void Start()
    {
        SetEnteringPlayers();
    }

    private void GameManager_NextStateEvent()
    {
        switch (GameManager.GetState())
        {
            case State.ShowingFirstWords:
            case State.ShowingSecondWords:
            case State.ShowingOutsider:
                AddShowingPlayers();
                break;
        }
    }

    private void AddShowingPlayers()
    {
        contentPanelHeigth = 0f;
        foreach (GameObject playerInfoObject in playerInfoObjects)
        {
            PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            PlayerInfoResizeAndSetParent(playerInfoObject, playerInfo);
            playerInfoObject.SetActive(true);
        }
    }

    public void SetEnteringPlayers()
    {
        contentPanelHeigth = 0f;
        // when we are restarting destroy old playerObjects (easier than cleaning)
        if (GameManager.GetPlayerNames() != null && GameManager.GetPlayerNames().Count > 0)
        {
            foreach (GameObject playerInfoObject in playerInfoObjects)
            {
                DestroyImmediate(playerInfoObject);
            }
            playerInfoObjects.Clear();
        }
        // and create new ones with prefilled names if we have any
        for (int i = 1; i <= maxPlayers; i++)
        {
            if (GameManager.GetPlayerNames() != null && GameManager.GetPlayerNames().Count > 0 && i <= GameManager.GetPlayerNames().Count)
            {
                AddEnteringPlayer(i, GameManager.GetPlayerNames()[i - 1]);
            }
            else
            {
                AddEnteringPlayer(i, "");
            }
        }
    }

    private void AddEnteringPlayer(int index, string playerName)
    {
        GameObject playerInfoObject = GameObject.Instantiate(playerInfoPrefab);
        playerInfoObjects.Add(playerInfoObject);
        PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
        playerInfo.SetIndexAndNameLabel(index);
        playerInfo.SetName(playerName);
        PlayerInfoResizeAndSetParent(playerInfoObject, playerInfo);
    }

    private void PlayerInfoResizeAndSetParent(GameObject playerInfoObject, PlayerInfo playerInfo)
    {
        playerInfo.Resize();
        playerInfoObject.transform.SetParent(contentPanel.transform);
        playerInfoObject.transform.localScale = new Vector3(1f, 1f, 1f);
        RectTransform phRectTransform = contentPanel.GetComponent<RectTransform>();
        contentPanelHeigth += playerInfoObject.GetComponent<RectTransform>().rect.height;
        contentPanelHeigth += spacing;
        phRectTransform.sizeDelta = new Vector2(phRectTransform.sizeDelta.x, contentPanelHeigth);
        /* position is solved by a Vertical Layout Group */
    }

    public bool ValidateInput()
    {
        int numberOfPlayers = 0;
        GameManager.GetPlayerNames().Clear();
        List<string> nonUnique = new List<string>();
        foreach (GameObject playerInfoObject in playerInfoObjects)
        {
            PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            if (playerInfo.HasName())
            {
                numberOfPlayers++;
                if (GameManager.GetPlayerNames().Contains(playerInfo.GetName()))
                {
                    nonUnique.Add(playerInfo.GetName());
                }
                else
                {
                    GameManager.GetPlayerNames().Add(playerInfo.GetName());
                }

            }
        }
        if (nonUnique.Count > 0)
        {
            StartCoroutine(ShowErrorMessage("De spelernamen moeten\n uniek zijn.\n" + nonUnique[0]));
            return false;
        }
        if (numberOfPlayers < 3)
        {
            StartCoroutine(ShowErrorMessage("Er moeten minstens\n3 spelers zijn"));
            return false;
        }
        return true;
    }

    IEnumerator ShowErrorMessage(string message)
    {
        errorMessage.GetComponent<TMP_Text>().text = message;
        errorMessage.SetActive(true);
        yield return new WaitForSeconds(4);
        errorMessage.SetActive(false);
    }

    public List<GameObject> GetPlayers()
    {
        List<GameObject> enteredPlayers = new List<GameObject>();
        int i = 1;
        foreach(GameObject playerInfoObject in playerInfoObjects)
        {
            PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            if (playerInfo.HasName())
            {
                playerInfo.SetIndexAndNameLabel(i++);
                enteredPlayers.Add(playerInfoObject);
            } else {
                Destroy(playerInfoObject);
            }
        }
        playerInfoObjects = enteredPlayers;
        return enteredPlayers;
    }

}
