using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���������� ������� ������ �� ��������������. (���������� ��������)
/// </summary>
public class EffectOnCharacteristicController : MonoBehaviour
{
    private static float speedAnimation = 20; // �������� ��������

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
        // ���������� ��������

        Vector3 newPos = Vector3.Lerp(rectTransform.localPosition, 
            targetPosition, speedAnimation * Time.deltaTime);

        rectTransform.localPosition = newPos;
    }


    /// <summary>
    /// �������� �������. (��������)
    /// </summary>
    public void ShowEffect(EffectSizePerCharacteristic effect)
    {
        rectTransform.sizeDelta = new Vector2((int)effect, (int)effect); // ������ ������
        targetPosition = endPosition; // ������������� ����� �������
    }


    /// <summary>
    /// ������ �������. (��������)
    /// </summary>
    public void HideEffect()
    {
        targetPosition = startPosition;
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
