using Globals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerPanelController : MonoBehaviour
{

    public GameObject contentPanel;
    public GameObject nameButtonObject;
    public TextMeshProUGUI nameButtonText;
    public GameObject oopsButtonObject;
    public GameObject readyButtonObject;

    private Button nameButton;
    private Button oopsButton;
    private Button readyButton;
    private GameObject playerInfoObject;
    private PlayerInfo playerInfo;

    void Start()
    {
        nameButton = nameButtonObject.GetComponent<Button>();
        oopsButton = oopsButtonObject.GetComponent<Button>();
        readyButton = readyButtonObject.GetComponent<Button>();
    }

    public void NextPlayer()
    {
        playerInfoObject = GameManager.GetNextPlayer();
        if (playerInfoObject != null) // is there a next player or are we going to the next state
        {
            playerInfoObject.SetActive(false);
            playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            Debug.Log("Got player " +  playerInfo.index);
            playerInfo.Resize();
            playerInfoObject.transform.SetParent(contentPanel.transform);
            playerInfoObject.transform.localScale = new Vector3(1f, 1f, 1f);

            nameButtonText.text = "Ik ben\n" + playerInfo.GetName();
            nameButtonObject.SetActive(true);
            oopsButtonObject.SetActive(false);
            readyButtonObject.SetActive(false);
        }
        else
        {
            GameManager.NextState();
        }
    }

    public void OnImPlayerX()
    {
        playerInfoObject.SetActive(true);
        nameButtonObject.SetActive(false);
        oopsButtonObject.SetActive(true);
        readyButtonObject.SetActive(true);
    }

    public void OnOops()
    {
        playerInfoObject.SetActive(false);
        nameButtonObject.SetActive(true);
        oopsButtonObject.SetActive(false);
        readyButtonObject.SetActive(false);
    }

    public void OnPlayerReady()
    {
        playerInfoObject.SetActive(false);
        NextPlayer();
    }
}
