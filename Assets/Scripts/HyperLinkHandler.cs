using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HyperLinkHandler : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // First, get the index of the link clicked. Each of the links in the text has its own index.
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, null);

        if (linkIndex != -1)
        {
            // Get the link info
            string linkId = text.textInfo.linkInfo[linkIndex].GetLinkID();

            Debug.Log($"URL clicked: linkInfo[{linkIndex}].id={linkId}");
            // Process the click
            if (linkId.StartsWith("http://") || linkId.StartsWith("https://"))
            {
                Application.OpenURL(linkId); // Open URL
            }
        }
    }
}
