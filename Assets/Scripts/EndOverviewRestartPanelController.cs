using Globals;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class EndOverviewRestartPanelController : MonoBehaviour
{

    public TMP_InputField outsiderInputField;
    public TMP_InputField insiderWordInputField;
    public TMP_InputField outsiderWordInputField;
    public TMP_Text wordsOriginText;

    private LocalizedString locWordsOriginText = new LocalizedString(GameManager.LOCALIZATION_TABLE_NAME, "EndOverviewRestartPanel-WordsOriginText");

    void OnEnable()
    {
        outsiderInputField.text = GameManager.GetOutsiderName();
        insiderWordInputField.text = GameManager.GetWordSet().InsiderWord;
        outsiderWordInputField.text = GameManager.GetWordSet().OutsiderWord;
        wordsOriginText.text = locWordsOriginText.GetLocalizedString(GameManager.GetWordsOrigin());
        LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
    }

    public void OnAgain()
    {
        GameManager.SetState(State.EnteringPlayers);
    }

    private void OnSelectedLocaleChanged(Locale locale)
    {
        wordsOriginText.text = locWordsOriginText.GetLocalizedString(GameManager.GetWordsOrigin());
    }

}
