using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowRulesToggle : MonoBehaviour
{
    private int showRulesAtStartup;
    private Toggle showRules;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showRulesAtStartup = PlayerPrefs.GetInt("ShowRulesAtStartup",1);
        showRules = GetComponent<Toggle>();
        showRules.isOn = (showRulesAtStartup == 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged()
    {
        if (showRules.isOn)
        {
            showRulesAtStartup = 1;
        }
        else
        {
            showRulesAtStartup = 0;
        }
        PlayerPrefs.SetInt("ShowRulesAtStartup", showRulesAtStartup);
        PlayerPrefs.Save();
    }
}
