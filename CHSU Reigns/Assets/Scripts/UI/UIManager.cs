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
    /// Устанавливает основной текст карты.
    /// </summary>
    public void SetTitle(string text)
    {
        title.text = text;
    }

    /// <summary>
    /// Показывает, как будет влиять свайп карты на характеристики. (активирует кружочки)
    /// </summary>
    public void ShowCardPerformanceForCharacteristics(Card card, SwipeType swipeType)
    {
        for (int i = 0; i < effectsOnCharacteristics.Length; i++)
        {
            // Влияние на данную характеристику
            int effect = card.GetCharacteristic(swipeType, 
                GameManager.instance.playerClass, 
                effectsOnCharacteristics[i].characteristicType);

            effect = Mathf.Abs(effect); // Смотрим по модулю

            if (effect == Characteristics.smallChange) // маленькое
            {
                effectsOnCharacteristics[i].controller.ShowEffect(
                    EffectOnCharacteristicController.EffectSizePerCharacteristic.Small);    
            }
            else // Значительное
            {
                effectsOnCharacteristics[i].controller.ShowEffect(
                    EffectOnCharacteristicController.EffectSizePerCharacteristic.Large);
            }
        }
    }

    /// <summary>
    /// Скрывает то, как будет влиять свайп карты на характеристики. (скрывает кружочки)
    /// </summary>
    public void HideCardPerformanceForCharacteristics()
    {
        for (int i = 0; i < effectsOnCharacteristics.Length; i++)
        {
            effectsOnCharacteristics[i].controller.HideEffect();
        }
    }
}
