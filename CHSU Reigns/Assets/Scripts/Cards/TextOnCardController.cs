using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
///  онтроллер текста на карте. (+ задний фон дл€ них)
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
    [SerializeField] private Vector3 startBackgroundPosition; // начальна€ позици€ фона
    [SerializeField] private Vector3 endBackgroundPosition; // конечна€ позици€ фона

    private bool isShowing; // текст показываетс€?

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
    /// ѕоказать текст.
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
    /// ”станавливает левый текст.
    /// </summary>
    public void SetLeftText(string text)
    {
        textLeft.text = text;
    }


    /// <summary>
    /// ”становливает правый текст.
    /// </summary>
    public void SetRightText(string text)
    {
        textRight.text = text;
    }


    /// <summary>
    /// —крыть текст.
    /// </summary>
    public void HideText() 
    {
        isShowing = false;
        textLeft.gameObject.SetActive(false);
        textRight.gameObject.SetActive(false);
    }
}
