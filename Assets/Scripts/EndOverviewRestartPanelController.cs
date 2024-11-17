using Globals;
using TMPro;
using UnityEngine;

public class EndOverviewRestartPanelController : MonoBehaviour
{

    public TMP_InputField outsiderInputField;
    public TMP_InputField insiderWordInputField;
    public TMP_InputField outsiderWordInputField;
    
    void OnEnable()
    {
        outsiderInputField.text = GameManager.GetOutsiderName();
        insiderWordInputField.text = GameManager.GetWordSet().InsiderWord;
        outsiderWordInputField.text = GameManager.GetWordSet().OutsiderWord;
    }

}