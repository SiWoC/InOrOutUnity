using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowRulesToggle : MonoBehaviour
{
    private Toggle showRules;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showRules = GetComponent<Toggle>();
        showRules.isOn = SettingsPanelController.GetShowRulesAtStartup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged()
    {
        SettingsPanelController.SetShowRulesAtStartup(showRules.isOn);
    }
}
