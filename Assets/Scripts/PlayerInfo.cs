using Globals;
using System;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public enum Role
    {
        Insider,
        Outsider
    }

    public TextMeshProUGUI textNameLabel;
    public TMP_InputField nameInputField;
    public GameObject buzzWordPanel;
    public TMP_InputField buzzWordInputField;
    public GameObject firstWordPanel;
    public TMP_InputField firstWordInputField;
    public GameObject secondWordPanel;
    public TMP_InputField secondWordInputField;
    public GameObject outsiderPanel;
    public TMP_InputField outsiderInputField;

    public int index;
    private Role role;

    public Role GetRole()
    {
        return role;
    }

    public void SetRole(Role newRole)
    {
        role = newRole;
    }

    public void Resize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        switch (GameManager.GetState())
        {
            case State.EnteringPlayers:
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 100);
                break;
            case State.ShowChosenWord:
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 200);
                break;
        }
        ActivatePanels();
    }

    private void ActivatePanels()
    {
        buzzWordPanel.SetActive(GameManager.GetState() == State.ShowChosenWord);
        firstWordPanel.SetActive(GameManager.GetState() == State.EnteringFirstWord
                                || GameManager.GetState() == State.ShowingFirstWords);
        secondWordPanel.SetActive(GameManager.GetState() == State.EnteringSecondWord
                                || GameManager.GetState() == State.ShowingSecondWords);
        outsiderPanel.SetActive(GameManager.GetState() == State.EnteringOutsider
                                || GameManager.GetState() == State.ShowingOutsider);
    }

    internal void SetIndexAndNameLabel(int i)
    {
        index = i;
        textNameLabel.text = "Speler " + i;
    }

    public string GetName()
    {
        return nameInputField.text;
    }
    internal bool HasName()
    {
        return !string.IsNullOrWhiteSpace(nameInputField.text);
    }

    internal void SetBuzzWord(string buzzWord)
    {
        buzzWordInputField.text = buzzWord;
    }
}
