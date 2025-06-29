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
    public GameObject showBuzzWordExplanationObject;
    public GameObject firstWordExplanationObject;
    public GameObject secondWordExplanationObject;
    public GameObject outsiderExplanationObject;
    public GameObject readyButtonObject;
    public GameObject passOnText;
    public GameObject oopsText;
    public GameObject nameButtonObject;
    public TextMeshProUGUI nameButtonText;
    public GameObject oopsButtonObject;

    private GameObject playerInfoObject;
    private PlayerInfo playerInfo;
    private string currentPlayerName;
    private LocalizedString locNameButtonText = new LocalizedString(GameManager.LOCALIZATION_TABLE_NAME, "SinglePlayerPanel-NameButtonText");

    void Awake()
    {
        GameManager.NextStateEvent += GameManager_NextStateEvent;
    }

    void Start()
    {
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
            ActivatePanels();
            playerInfoObject.SetActive(false);
            playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
            playerInfo.Resize();
            playerInfoObject.transform.SetParent(contentPanel.transform);
            playerInfoObject.transform.localScale = new Vector3(1f, 1f, 1f);

            passOnText.SetActive(true);
            oopsText.SetActive(false);
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

    private void ActivatePanels()
    {
        showBuzzWordExplanationObject.SetActive(GameManager.GetState() == State.ShowChosenWord);
        firstWordExplanationObject.SetActive(GameManager.GetState() == State.EnteringFirstWord);
        secondWordExplanationObject.SetActive(GameManager.GetState() == State.EnteringSecondWord);
        outsiderExplanationObject.SetActive(GameManager.GetState() == State.EnteringOutsider);
    }


    public void OnImPlayerX()
    {
        playerInfoObject.SetActive(true);
        nameButtonObject.SetActive(false);
        passOnText.SetActive(false);
        oopsText.SetActive(true);
        oopsButtonObject.SetActive(true);
        readyButtonObject.SetActive(true);
    }

    public void OnOops()
    {
        playerInfo.ClearEnabledWord();
        playerInfoObject.SetActive(false);
        passOnText.SetActive(true);
        oopsText.SetActive(false);
        nameButtonObject.SetActive(true);
        oopsButtonObject.SetActive(false);
        readyButtonObject.SetActive(false);
    }

    private void SetNameButtonText()
    {
        nameButtonText.text = locNameButtonText.GetLocalizedString(currentPlayerName);
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
    }

}
