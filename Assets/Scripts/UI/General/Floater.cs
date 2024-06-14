using UnityEngine;

public class Floater : MonoBehaviour
{
    public float floatHeight = 10f, floatSpeed = 1f;

    private RectTransform rectTransform;
    private float startY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}
