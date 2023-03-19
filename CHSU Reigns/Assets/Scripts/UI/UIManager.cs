using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// UI manager.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Card holder")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private EffectOnCharacteristic[] effectsOnCharacteristics;


    private void Start()
    {
        instance = this;
    }


    /// <summary>
    /// ������������� �������� ����� �����.
    /// </summary>
    public void SetTitle(string text)
    {
        title.text = text;
    }

    /// <summary>
    /// ����������, ��� ����� ������ ����� ����� �� ��������������. (���������� ��������)
    /// </summary>
    public void ShowCardPerformanceForCharacteristics(Card card, SwipeType swipeType)
    {
        for (int i = 0; i < effectsOnCharacteristics.Length; i++)
        {
            // ������� �� ������ ��������������
            int effect = card.GetCharacteristic(swipeType, 
                GameManager.instance.playerClass, 
                effectsOnCharacteristics[i].characteristicType);

            effect = Mathf.Abs(effect); // ������� �� ������

            if (effect == Characteristics.smallChange) // ���������
            {
                effectsOnCharacteristics[i].controller.ShowEffect(
                    EffectOnCharacteristicController.EffectSizePerCharacteristic.Small);    
            }
            else // ������������
            {
                effectsOnCharacteristics[i].controller.ShowEffect(
                    EffectOnCharacteristicController.EffectSizePerCharacteristic.Large);
            }
        }
    }

    /// <summary>
    /// �������� ��, ��� ����� ������ ����� ����� �� ��������������. (�������� ��������)
    /// </summary>
    public void HideCardPerformanceForCharacteristics()
    {
        for (int i = 0; i < effectsOnCharacteristics.Length; i++)
        {
            effectsOnCharacteristics[i].controller.HideEffect();
        }
    }
}
