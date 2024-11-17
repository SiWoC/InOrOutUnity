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

    private float singleFieldHeigth = 100f;

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
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, singleFieldHeigth * numFields);

        nameInputField.interactable = (GameManager.GetState() == State.EnteringPlayers);
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
        outsiderInputField.interactable = (GameManager.GetState() == State.EnteringOutsider);

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
        outsiderInputField.text = outsiderInputField.text.Trim();
        return !string.IsNullOrWhiteSpace(outsiderInputField.text);
    }

    internal void SetBuzzWord(string buzzWord)
    {
        buzzWordInputField.text = buzzWord;
    }
}
