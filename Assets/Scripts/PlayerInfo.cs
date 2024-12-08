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
    public GameObject buzzWordOrFirstPanel;
    public GameObject buzzWordPanel;
    public TMP_InputField buzzWordInputField;
    public GameObject firstWordPanel;
    public TMP_InputField firstWordInputField;
    public GameObject secondWordPanel;
    public TMP_InputField secondWordInputField;
    public GameObject outsiderPanel;
    public TMP_InputField outsiderInputField;
    public GameObject bottomLinePanel;
    public TMP_Dropdown outsiderDropdown;

    public int index;

    private float singleFieldHeight = 80f;
    private float bottomLineHeight = 20f;

    public void Resize()
    {
        int numFields = 1;
        switch (GameManager.GetState())
        {
            case State.EnteringPlayers:
                numFields = 1;
                break;
            case State.ShowChosenWord:
            case State.EnteringFirstWord:
            case State.ShowingFirstWords:
                numFields = 2;
                break;
            case State.EnteringSecondWord:
            case State.ShowingSecondWords:
                numFields = 3;
                break;
            case State.EnteringOutsider:
            case State.ShowingOutsider:
                numFields = 4;
                break;
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (singleFieldHeight * numFields) + bottomLineHeight);

        nameInputField.interactable = (GameManager.GetState() == State.EnteringPlayers);
        buzzWordOrFirstPanel.SetActive(GameManager.GetState() == State.ShowChosenWord
                                || GameManager.GetState() == State.EnteringFirstWord
                                || GameManager.GetState() == State.ShowingFirstWords
                                || GameManager.GetState() == State.EnteringSecondWord
                                || GameManager.GetState() == State.ShowingSecondWords
                                || GameManager.GetState() == State.EnteringOutsider
                                || GameManager.GetState() == State.ShowingOutsider);
        buzzWordPanel.SetActive(GameManager.GetState() == State.ShowChosenWord);
        firstWordPanel.SetActive(GameManager.GetState() == State.EnteringFirstWord
                                || GameManager.GetState() == State.ShowingFirstWords
                                || GameManager.GetState() == State.EnteringSecondWord
                                || GameManager.GetState() == State.ShowingSecondWords
                                || GameManager.GetState() == State.EnteringOutsider
                                || GameManager.GetState() == State.ShowingOutsider);
        firstWordInputField.interactable = (GameManager.GetState() == State.EnteringFirstWord);
        secondWordPanel.SetActive(GameManager.GetState() == State.EnteringSecondWord
                                || GameManager.GetState() == State.ShowingSecondWords
                                || GameManager.GetState() == State.EnteringOutsider
                                || GameManager.GetState() == State.ShowingOutsider);
        secondWordInputField.interactable = (GameManager.GetState() == State.EnteringSecondWord);
        outsiderPanel.SetActive(GameManager.GetState() == State.EnteringOutsider
                                || GameManager.GetState() == State.ShowingOutsider);
        outsiderDropdown.interactable = (GameManager.GetState() == State.EnteringOutsider);

        if (GameManager.GetState() == State.EnteringOutsider)
        {
            outsiderDropdown.ClearOptions();
            outsiderDropdown.AddOptions(GameManager.GetPlayerNames());
        }
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

    public void SetName(string nameIn)
    {
        nameInputField.text = nameIn;
    }

    internal bool HasName()
    {
        nameInputField.text = nameInputField.text.Trim();
        return !string.IsNullOrWhiteSpace(nameInputField.text);
    }

    internal bool HasFirstWord()
    {
        firstWordInputField.text = firstWordInputField.text.Trim();
        return !string.IsNullOrWhiteSpace(firstWordInputField.text);
    }

    internal bool HasSecondWord()
    {
        secondWordInputField.text = secondWordInputField.text.Trim();
        return !string.IsNullOrWhiteSpace(secondWordInputField.text);
    }

    internal bool HasOutsider()
    {
        return true;
        /*
        outsiderInputField.text = outsiderInputField.text.Trim();
        return !string.IsNullOrWhiteSpace(outsiderInputField.text);
        */
    }

    internal void SetBuzzWord(string buzzWord)
    {
        buzzWordInputField.text = buzzWord;
    }
}
