using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���������� ������� ������ �� ��������������. (���������� ��������)
/// </summary>
public class EffectOnCharacteristicController : MonoBehaviour
{
    private static float speedAnimation = 30;
    private RectTransform rectTransform;
    private Vector2 newSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        HideEffect();
    }

    private void Update()
    {
        // ������������ �������� �� �������� (��������)
        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, newSize, speedAnimation * Time.deltaTime);
    }


    /// <summary>
    /// �������� �������. (��������)
    /// </summary>
    public void ShowEffect(EffectSizePerCharacteristic effect)
    {
        newSize = new Vector2((int)effect, (int)effect); // ������ ������

        if (!enabled)
            rectTransform.sizeDelta = Vector2.zero;

        gameObject.SetActive(true);
    }


    /// <summary>
    /// ������ �������. (��������)
    /// </summary>
    public void HideEffect()
    {
        rectTransform.sizeDelta = Vector2.zero;
        gameObject.SetActive(false);
    }


    /// <summary>
    /// ������ ������� �� ��������������. (������ ��������)
    /// </summary>
    public enum EffectSizePerCharacteristic : int
    {
        Small = 18,
        Large = 28
    }
}
