using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllPlayersPanelController : MonoBehaviour
{

    public GameObject contentPanel;
    public GameObject playerInfoPrefab;
    public GameObject readyButton;

    private float contentPanelHeigth = 0;
    private float singleFieldHeigth = 100;
    private List<GameObject> playerInfoObjects = new List<GameObject>();
    private Button button;

    void Start()
    {
        // phRectTransform = contentPanel.GetComponent<RectTransform>();
        for (int i = 1; i <= 10; i++)
        {
            AddPlayerInfo(i);
        }
        button = readyButton.GetComponent<Button>();
        button.interactable = true;
    }

    private void AddPlayerInfo(int i)
    {
        GameObject playerInfoObject = GameObject.Instantiate(playerInfoPrefab);
        playerInfoObjects.Add(playerInfoObject);
        PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
        playerInfo.SetIndexAndNameLabel(i);
        playerInfo.Resize();
        playerInfoObject.transform.SetParent(contentPanel.transform);
        playerInfoObject.transform.localScale = new Vector3(1f, 1f, 1f);
        RectTransform phRectTransform = contentPanel.GetComponent<RectTransform>();
        contentPanelHeigth += singleFieldHeigth;
        phRectTransform.sizeDelta = new Vector2(phRectTransform.sizeDelta.x, contentPanelHeigth);
        /*
        statisticsGroupObject.transform.SetParent(contentPanel.transform);
        statisticsGroupObject.transform.localScale = new Vector3(1f, 1f, 1f);
        RectTransform phRectTransform = contentPanel.GetComponent<RectTransform>();
        contentPanelHeigth += statisticsGroupHeight;
        phRectTransform.sizeDelta = new Vector2(phRectTransform.sizeDelta.x, contentPanelHeigth);

         
        RectTransform playerInfoPanelRT = playerInfoPanel.GetComponent<RectTransform>();
        playerInfoPanelRT.anchoredPosition = new Vector3(0f, -1f * ycoord, 0f);
         */
    }

    public void CheckNumberOfPlayers()
    {
        int i = 1;
        foreach (GameObject playerInfoObject in playerInfoObjects)
        {
            PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            if (playerInfo.HasName())
            {
                i++;
            }
        }
        Debug.Log("NumberOfPlayers " + i);
        if (i > 2)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
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
                Debug.Log("Adding [" + playerInfo.nameInputField.text + "]");
                enteredPlayers.Add(playerInfoObject);
            } else {
                Destroy(playerInfoObject);
            }
        }
        return enteredPlayers;
    }

}
