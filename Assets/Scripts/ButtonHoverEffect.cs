using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Touch Settings")]
    [Range(1f, 1.2f)]
    public float pressScale = 1.05f;
    [Range(1f, 20f)]
    public float animationSpeed = 10f;

    private Vector3 originalScale;
    private bool isPressed = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPressed)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * pressScale, Time.deltaTime * animationSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * animationSpeed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
} 