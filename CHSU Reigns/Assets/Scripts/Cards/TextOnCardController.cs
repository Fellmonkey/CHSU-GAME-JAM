using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOnCardController : MonoBehaviour
{

    [Header("Speed animation")]
    [SerializeField] private float speedAnimation;

    [Header("Objects")]
    [SerializeField] private Transform backgroundTransform;
    [SerializeField] private TextMeshProUGUI textLeft;
    [SerializeField] private TextMeshProUGUI textRight;

    [Header("Positions")]
    [SerializeField] private Vector3 startBackgroundPosition;
    [SerializeField] private Vector3 endBackgroundPosition;

    private bool isShowing;

    private void Start()
    {
        backgroundTransform.localPosition = startBackgroundPosition;
        isShowing = false;
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

    public void SetLeftText(string text)
    {
        textLeft.text = text;
    }

    public void SetRightText(string text)
    {
        textRight.text = text;
    }

    public void HideText() 
    {
        isShowing = false;
        textLeft.gameObject.SetActive(false);
        textRight.gameObject.SetActive(false);
    }


}
