using Globals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class SettingsPanelController : MonoBehaviour
{
    private const string PREF_OPENAIAPIKEY = "OpenAIApiKey";
    private const string PREF_CLAUDEAPIKEY = "ClaudeApiKey";
    private const string PREF_GENERATORTYPE = "GeneratorType";
    private const string PREF_SHOWRULESATSTARTUP = "ShowRulesAtStartup";
    private const string PREF_LOCALE = "Locale";

    public GameObject settingsPanel;
    public TMP_InputField openAIApiKeyInputField;
    public TMP_InputField claudeApiKeyInputField;
    public Toggle hardCodedToggle;
    public Toggle openAIToggle;
    public Toggle claudeToggle;
    public GameObject settingsButton;
    public GameObject restartConfirmationPanel;

    void Start()
    {
        switch (GameManager.GetGeneratorType())
        {
            case GeneratorType.File:
                hardCodedToggle.isOn = true;
                break;
            case GeneratorType.OpenAI:
                openAIToggle.isOn = true;
                break;
            case GeneratorType.Claude:
                claudeToggle.isOn = true;
                break;
            default:
                break;
        }

        openAIApiKeyInputField.text = PlayerPrefs.GetString(PREF_OPENAIAPIKEY, string.Empty);
        claudeApiKeyInputField.text = PlayerPrefs.GetString(PREF_CLAUDEAPIKEY, string.Empty);
    }

    // Update is called once per frame
    void Update()
    {
        if (openAIApiKeyInputField.text.Equals(string.Empty))
        {
            if (openAIToggle.isOn)
            {
                openAIToggle.isOn = false;
                hardCodedToggle.isOn = true;
            }
            openAIToggle.interactable = false;
        }
        else
        {
            openAIToggle.interactable = true;
        }

        if (claudeApiKeyInputField.text.Equals(string.Empty))
        {
            if (claudeToggle.isOn)
            {
                claudeToggle.isOn = false;
                hardCodedToggle.isOn = true;
            }
            claudeToggle.interactable = false;
        }
        else
        {
            claudeToggle.interactable = true;
        }
    }

    public void OnReady()
    {
        GeneratorType newGeneratorType;
        PlayerPrefs.SetString(PREF_OPENAIAPIKEY, openAIApiKeyInputField.text);
        PlayerPrefs.SetString(PREF_CLAUDEAPIKEY, claudeApiKeyInputField.text);
        if (openAIToggle.isOn && !openAIApiKeyInputField.text.Equals(string.Empty))
        {
            newGeneratorType = GeneratorType.OpenAI;
        } 
        else if (claudeToggle.isOn && !claudeApiKeyInputField.text.Equals (string.Empty)) 
        {
            newGeneratorType = GeneratorType.Claude;
        } 
        else  
        {
            newGeneratorType = GeneratorType.File;
        }
        GameManager.SetGeneratorType(newGeneratorType);
        PlayerPrefs.SetInt(PREF_GENERATORTYPE, (int)newGeneratorType);
        PlayerPrefs.Save();
        settingsPanel.SetActive(false);
        settingsButton.SetActive(true);
    }

    public void OnRestart()
    {
        restartConfirmationPanel.SetActive(true);
    }

    public void OnCancelRestart()
    {
        restartConfirmationPanel.SetActive(false);
    }

    public void OnReallyRestart()
    {
        restartConfirmationPanel.SetActive(false);
        OnReady();
        GameManager.SetState(State.EnteringPlayers);
    }

    public void OnDutch()
    {
        SetLocale("nl");
    }

    public void OnEnglish()
    {
        SetLocale("en");
    }

    public static bool GetShowRulesAtStartup()
    {
        return (PlayerPrefs.GetInt(PREF_SHOWRULESATSTARTUP, 1) == 1);
    }

    public static void SetShowRulesAtStartup(bool showRulesAtStartup)
    {
        if (showRulesAtStartup)
        {
            PlayerPrefs.SetInt(PREF_SHOWRULESATSTARTUP, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PREF_SHOWRULESATSTARTUP, 0);
        }
        PlayerPrefs.Save();
    }

    public static GeneratorType GetGeneratorType()
    {
        return (GeneratorType)PlayerPrefs.GetInt(PREF_GENERATORTYPE, 0);
    }

    internal static string GetOpenAIApiKey()
    {
        return PlayerPrefs.GetString(PREF_OPENAIAPIKEY);
    }

    internal static string GetClaudeApiKey()
    {
        return PlayerPrefs.GetString(PREF_CLAUDEAPIKEY);
    }

    public static string GetLocale()
    {
        string storedLocale = PlayerPrefs.GetString(PREF_LOCALE, null);
        if (storedLocale != null)
        {
            return storedLocale;
        }
        
        // Check system locale
        if (Application.systemLanguage == SystemLanguage.Dutch)
        {
            return "nl";
        }
        return "en";
    }

    public static void SetLocale(string locale)
    {
        PlayerPrefs.SetString(PREF_LOCALE, locale);
        PlayerPrefs.Save();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale);
    }
}
