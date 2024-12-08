using Globals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    public TMP_InputField openAIApiKeyInputField;
    public TMP_InputField claudeApiKeyInputField;
    public Toggle hardCodedToggle;
    public Toggle openAIToggle;
    public Toggle claudeToggle;
    public GameObject settingsButton;

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

        openAIApiKeyInputField.text = PlayerPrefs.GetString("OpenAIApiKey", string.Empty);
        claudeApiKeyInputField.text = PlayerPrefs.GetString("ClaudeApiKey", string.Empty);
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
        PlayerPrefs.SetString("OpenAIApiKey", openAIApiKeyInputField.text);
        PlayerPrefs.SetString("ClaudeApiKey", claudeApiKeyInputField.text);
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
        PlayerPrefs.SetInt("GeneratorType", (int)newGeneratorType);
        PlayerPrefs.Save();
        settingsPanel.SetActive(false);
        settingsButton.SetActive(true);
    }
}
