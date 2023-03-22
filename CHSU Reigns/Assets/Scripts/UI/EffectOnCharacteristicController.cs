using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Контроллер влияния свайпа на характеристику. (контроллер кружочка)
/// </summary>
public class EffectOnCharacteristicController : MonoBehaviour
{
    private static float speedAnimation = 20; // Скорость анимации

    [Header("Animation positions")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    private Vector3 targetPosition;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = startPosition;
        targetPosition = startPosition;
    }

    private void Update()
    {
        // перемещаем кружочек

        Vector3 newPos = Vector3.Lerp(rectTransform.localPosition, 
            targetPosition, speedAnimation * Time.deltaTime);

        rectTransform.localPosition = newPos;
    }


    /// <summary>
    /// Показать влияние. (кружочек)
    /// </summary>
    public void ShowEffect(EffectSizePerCharacteristic effect)
    {
        rectTransform.sizeDelta = new Vector2((int)effect, (int)effect); // задаем размер
        targetPosition = endPosition; // Устанавливаем новую позицию
    }


    /// <summary>
    /// Скрыть влияние. (кружочек)
    /// </summary>
    public void HideEffect()
    {
        targetPosition = startPosition;
    }

    /// <summary>
    /// Размер влияния на характеристику. (размер кружочка)
    /// </summary>
    public enum EffectSizePerCharacteristic : int
    {
        Small = 18,
        Large = 28
    }
}
