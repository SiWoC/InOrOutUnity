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
    private float singleFieldHeigth = 100f;
    private float spacing = 15f;
    private List<GameObject> playerInfoObjects = new List<GameObject>();
    private int maxPlayers = 10;
    List<string> playerNames = new List<string>();

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
                AddShowingPlayers(2);
                break;
            case State.ShowingSecondWords:
                AddShowingPlayers(3);
                break;
            case State.ShowingOutsider:
                AddShowingPlayers(4);
                break;
        }
    }

    private void AddShowingPlayers(int numFields)
    {
        contentPanelHeigth = 0;
        foreach (GameObject playerInfoObject in playerInfoObjects)
        {
            PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            PlayerInfoResizeAndSetParent(numFields, playerInfoObject, playerInfo);
            playerInfoObject.SetActive(true);
        }
    }

    public void SetEnteringPlayers()
    {
        // when we are restarting destroy old playerObjects (easier than cleaning)
        if (playerNames != null && playerNames.Count > 0)
        {
            Debug.Log("playernames " + playerNames.Count + " " + playerNames[0]);
            foreach (GameObject playerInfoObject in playerInfoObjects)
            {
                DestroyImmediate(playerInfoObject);
            }
            playerInfoObjects.Clear();
        }
        Debug.Log("objects left " + playerInfoObjects.Count);
        // and create new ones with prefilled names if we have any
        for (int i = 1; i <= maxPlayers; i++)
        {
            if (playerNames != null && playerNames.Count > 0 && i <= playerNames.Count)
            {
                AddEnteringPlayer(i, 1, playerNames[i - 1]);
            }
            else
            {
                AddEnteringPlayer(i, 1, "");
            }
        }
    }

    private void AddEnteringPlayer(int index, int numFields, string playerName)
    {
        GameObject playerInfoObject = GameObject.Instantiate(playerInfoPrefab);
        playerInfoObjects.Add(playerInfoObject);
        PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
        playerInfo.SetIndexAndNameLabel(index);
        playerInfo.SetName(playerName);
        PlayerInfoResizeAndSetParent(numFields, playerInfoObject, playerInfo);
    }

    private void PlayerInfoResizeAndSetParent(int numFields, GameObject playerInfoObject, PlayerInfo playerInfo)
    {
        playerInfo.Resize();
        playerInfoObject.transform.SetParent(contentPanel.transform);
        playerInfoObject.transform.localScale = new Vector3(1f, 1f, 1f);
        RectTransform phRectTransform = contentPanel.GetComponent<RectTransform>();
        contentPanelHeigth += singleFieldHeigth * numFields;
        contentPanelHeigth += spacing;
        phRectTransform.sizeDelta = new Vector2(phRectTransform.sizeDelta.x, contentPanelHeigth);
        /* position is solved by a Vertical Layout Group */
    }

    public bool ValidateInput()
    {
        int numberOfPlayers = 0;
        playerNames.Clear();
        List<string> nonUnique = new List<string>();
        foreach (GameObject playerInfoObject in playerInfoObjects)
        {
            PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            if (playerInfo.HasName())
            {
                numberOfPlayers++;
                if (playerNames.Contains(playerInfo.GetName()))
                {
                    nonUnique.Add(playerInfo.GetName());
                }
                else
                {
                    playerNames.Add(playerInfo.GetName());
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
