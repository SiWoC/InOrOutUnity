using Globals;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SinglePlayerPanelController : MonoBehaviour
{

    public GameObject contentPanel;
    public GameObject nameButtonObject;
    public TextMeshProUGUI nameButtonText;
    public TextMeshProUGUI nextOopsText;
    public GameObject oopsButtonObject;
    public GameObject readyButtonObject;

    private Button nameButton;
    private Button oopsButton;
    private Button readyButton;
    private GameObject playerInfoObject;
    private PlayerInfo playerInfo;
    private string currentPlayerName;
    private LocalizedString locNameButtonText = new LocalizedString(GameManager.LOCALIZATION_TABLE_NAME, "SinglePlayerPanel-NameButtonText");
    private LocalizedString locTextNextOopsNext = new LocalizedString(GameManager.LOCALIZATION_TABLE_NAME, "SinglePlayerPanel-TextNextOopsNext");
    private LocalizedString locTextNextOopsOops = new LocalizedString(GameManager.LOCALIZATION_TABLE_NAME, "SinglePlayerPanel-TextNextOopsOops");

    void Awake()
    {
        GameManager.NextStateEvent += GameManager_NextStateEvent;
    }

    void Start()
    {
        nameButton = nameButtonObject.GetComponent<Button>();
        oopsButton = oopsButtonObject.GetComponent<Button>();
        readyButton = readyButtonObject.GetComponent<Button>();
    }

    void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
    }

    private void GameManager_NextStateEvent()
    {
        switch (GameManager.GetState())
        {
            case State.ShowChosenWord:
            case State.EnteringFirstWord:
            case State.EnteringSecondWord:
            case State.EnteringOutsider:
                NextPlayer();
                break;
        }
    }

    public void NextPlayer()
    {
        playerInfoObject = GameManager.GetNextPlayer();
        if (playerInfoObject != null) // is there a next player or are we going to the next state
        {
            playerInfoObject.SetActive(false);
            playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            playerInfo.Resize();
            playerInfoObject.transform.SetParent(contentPanel.transform);
            playerInfoObject.transform.localScale = new Vector3(1f, 1f, 1f);

            SetNextOopsText(true);
            currentPlayerName = playerInfo.GetName();
            SetNameButtonText();
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
        SetNextOopsText(false);
        oopsButtonObject.SetActive(true);
        readyButtonObject.SetActive(true);
    }

    public void OnOops()
    {
        playerInfoObject.SetActive(false);
        SetNextOopsText(true);
        nameButtonObject.SetActive(true);
        oopsButtonObject.SetActive(false);
        readyButtonObject.SetActive(false);
    }

    private void SetNameButtonText()
    {
        nameButtonText.text = locNameButtonText.GetLocalizedString(currentPlayerName);
    }

    private void SetNextOopsText(bool useNextText)
    {
        if (useNextText)
        {
            nextOopsText.text = locTextNextOopsNext.GetLocalizedString();
        }
        else
        {
            nextOopsText.text = locTextNextOopsOops.GetLocalizedString();
        }
    }

    public void OnPlayerReady()
    {
        bool filled = true;
        switch (GameManager.GetState())
        {
            case State.EnteringFirstWord:
                filled = playerInfo.HasFirstWord();
                break;
            case State.EnteringSecondWord:
                filled = playerInfo.HasSecondWord();
                break;
            case State.EnteringOutsider:
                filled = playerInfo.HasOutsider();
                break;
        }
        if (filled)
        {
            playerInfoObject.SetActive(false);
            NextPlayer();
        }
    }

    private void OnSelectedLocaleChanged(Locale locale)
    {
        SetNameButtonText();
        SetNextOopsText(nameButtonObject.activeSelf);
    }

}
