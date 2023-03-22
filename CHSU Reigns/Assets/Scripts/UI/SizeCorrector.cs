using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Изменяет размер карт. (Под разные экраны)
/// </summary>
public class SizeCorrector : MonoBehaviour
{
    private static float Aspect;

    static SizeCorrector()
    {
        // 0.5625f = (720/1280)
        Aspect = 0.5625f - Screen.width / (float)Screen.height;
    }

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float size = rectTransform.sizeDelta.x - (rectTransform.sizeDelta.x * Aspect);
        Vector2 sizeDelta = new Vector2(size, size);
        rectTransform.sizeDelta = sizeDelta;
        Destroy(this);
    }
}
