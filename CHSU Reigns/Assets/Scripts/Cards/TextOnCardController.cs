using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Контроллер текста на карте. (+ задний фон для них)
/// </summary>
public class TextOnCardController : MonoBehaviour
{
    [Header("Speed animation")]
    [SerializeField] private float speedAnimation; // скорость анимации заднего фона (чтобы лучше читать текст)

    [Header("Objects")]
    [SerializeField] private Transform backgroundTransform; // задний фон
    [SerializeField] private TextMeshProUGUI textLeft; // левый текст
    [SerializeField] private TextMeshProUGUI textRight; // правый текст

    [Header("Positions")]
    [SerializeField] private Vector3 startBackgroundPosition; // начальная позици¤ фона
    [SerializeField] private Vector3 endBackgroundPosition; // конечная позиция фона

    private bool isShowing; // текст показывается?

    private void Start()
    {
        backgroundTransform.localPosition = startBackgroundPosition;
        isShowing = false;

        textLeft.gameObject.SetActive(false);
        textRight.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isShowing)
        {
            Vector3 v = Vector3.Lerp(backgroundTransform.localPosition, endBackgroundPosition, speedAnimation * Time.deltaTime);
            backgroundTransform.localPosition = v;
        }
        else
        {
            Vector3 v = Vector3.Lerp(backgroundTransform.localPosition, startBackgroundPosition, speedAnimation * Time.deltaTime);
            backgroundTransform.localPosition = v;
        }

        backgroundTransform.rotation = Quaternion.identity;
        textLeft.gameObject.transform.rotation = Quaternion.identity;
        textRight.gameObject.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Показать текст.
    /// </summary>
    public void ShowText(SwipeType swipe)
    {
        isShowing = true;

        if (swipe == SwipeType.Left)
        {
            textLeft.gameObject.SetActive(true);
            textRight.gameObject.SetActive(false);
        }
        else
        {
            textLeft.gameObject.SetActive(false);
            textRight.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Устанавливает левый текст.
    /// </summary>
    public void SetLeftText(string text)
    {
        textLeft.text = text;
    }

    /// <summary>
    /// Установливает правый текст.
    /// </summary>
    public void SetRightText(string text)
    {
        textRight.text = text;
    }

    /// <summary>
    /// Скрыть текст.
    /// </summary>
    public void HideText()
    {
        isShowing = false;
        textLeft.gameObject.SetActive(false);
        textRight.gameObject.SetActive(false);
    }
}
