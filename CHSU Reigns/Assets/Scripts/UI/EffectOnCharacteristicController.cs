using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Контроллер влияния свайпа на характеристику. (контроллер кружочка)
/// </summary>
public class EffectOnCharacteristicController : MonoBehaviour
{
    private static float speedAnimation = 25;
    private RectTransform rectTransform;
    private Vector2 newSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        HideEffect();
    }

    private void Update()
    {
        // увеличивание кружочка со временем (анимация)
        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, newSize, speedAnimation * Time.deltaTime);
    }


    /// <summary>
    /// Показать влияние. (кружочек)
    /// </summary>
    public void ShowEffect(EffectSizePerCharacteristic effect)
    {
        newSize = new Vector2((int)effect, (int)effect); // задаем размер

        if (!enabled)
            rectTransform.sizeDelta = Vector2.zero;

        gameObject.SetActive(true);
    }


    /// <summary>
    /// Скрыть влияние. (кружочек)
    /// </summary>
    public void HideEffect()
    {
        rectTransform.sizeDelta = Vector2.zero;
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Размер влияния на характеристику. (размер кружочка)
    /// </summary>
    public enum EffectSizePerCharacteristic : int
    {
        Normal = 18,
        Large = 28
    }
}
