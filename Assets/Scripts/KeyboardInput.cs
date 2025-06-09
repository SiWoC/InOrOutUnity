using UnityEngine;
using TMPro;

public class KeyboardInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public System.Action OnSubmit;

    private TouchScreenKeyboard _keyboard;
    private bool _wasVisible = false;

    /*
    private bool _keyboardVisible = false;
    private TouchScreenKeyboard _keyboard;
    */

    void Update()
    {
        var currentKeyboard = inputField.touchScreenKeyboard;
        if (currentKeyboard != null)
        {
            // New keyboard instance
            if (_keyboard != currentKeyboard)
            {
                string keyboardId = currentKeyboard.GetHashCode().ToString("X4");
                Debug.Log($"[{keyboardId}] New keyboard instance for field with text: {inputField.text}");
                _keyboard = currentKeyboard;
                _wasVisible = false;
            }

            // Status changed
            if (currentKeyboard.status == TouchScreenKeyboard.Status.Visible)
            {
                _wasVisible = true;
            }
            else if (currentKeyboard.status == TouchScreenKeyboard.Status.Done && _wasVisible)
            {
                string keyboardId = currentKeyboard.GetHashCode().ToString("X4");
                Debug.Log($"[{keyboardId}] Keyboard done with text: {currentKeyboard.text}");
                OnSubmit?.Invoke();
                _wasVisible = false;
                _keyboard = null;
            }
        }
        /*
        if (TouchScreenKeyboard.visible != _keyboardVisible)
        {
            Debug.Log($"Keyboard visibility changed to: {TouchScreenKeyboard.visible}");
            _keyboardVisible = TouchScreenKeyboard.visible;

            if (!_keyboardVisible && _keyboard != null) // Keyboard is closed
            {
                string submittedText = _keyboard.text;
                Debug.Log($"Keyboard closed with text: {submittedText}");
                if (!string.IsNullOrEmpty(submittedText))
                {
                    inputField.text = submittedText;
                    OnSubmit?.Invoke();
                    Debug.Log("Submitted Text: " + submittedText);
                }
                else
                {
                    Debug.Log("Keyboard dismissed without input");
                }
                _keyboard = null;
            }
        }

        // Store keyboard reference when input field is focused
        if (_keyboardVisible && _keyboard == null && inputField.isFocused)
        {
            _keyboard = inputField.touchScreenKeyboard;
            Debug.Log($"Got keyboard reference for field with text: {inputField.text}");
        }
        */
    }
} 