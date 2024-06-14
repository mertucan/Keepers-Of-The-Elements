using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public Color hoverColor = Color.red;
    private Color originalColor;
    public AudioSource audioSource;
    public AudioClip hoverClip;

    private void Start()
    {
        originalColor = text.color;

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
        if (audioSource != null && hoverClip != null) audioSource.PlayOneShot(hoverClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = originalColor;
    }
}
