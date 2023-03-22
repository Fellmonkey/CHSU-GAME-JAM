using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// UI manager.
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [Header("Characteristics")]
    [SerializeField] private Image respectImage;
    [SerializeField] private Image knowledgeImage;
    [SerializeField] private Image moneyImage;
    [SerializeField] private Image healthImage;

    [Header("Card holder")]
    [SerializeField] private TitleTextController titleController;
    [SerializeField] private EffectOnCharacteristicController respectEffect;
    [SerializeField] private EffectOnCharacteristicController knowledgeEffect;
    [SerializeField] private EffectOnCharacteristicController moneyEffect;
    [SerializeField] private EffectOnCharacteristicController healthEffect;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI namePlayer;

    [Header("Animation")]
    [SerializeField] private Animator[] characteristicsEffect;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Устанавливает основной текст карты.
    /// </summary>
    public static void SetTitle(string text)
    {
        instance.titleController.SetText(text);
    }

    /// <summary>
    /// Показывает, как будет влиять свайп карты на характеристики. (активирует кружочки)
    /// </summary>
    public static void ShowCardPerformanceForCharacteristics(Card card, SwipeType swipeType)
    {
        PlayerClass playerClass = GameManager.GetPlayerClass();

        void performanceForCharacteristics(EffectOnCharacteristicController controller, CharacteristicType type)
        {
            // Влияние на данную характеристику
            int effect = card.GetCharacteristic(swipeType, playerClass, type);

            if (effect == 0)
            {
                return;
            }

            effect = Mathf.Abs(effect); // Смотрим по модулю

            if (effect == Characteristics.smallChange) // маленькое
            {
                controller.ShowEffect(
                    EffectOnCharacteristicController.EffectSizePerCharacteristic.Small);
            }
            else // Значительное
            {
                controller.ShowEffect(
                    EffectOnCharacteristicController.EffectSizePerCharacteristic.Large);
            }
        }

        performanceForCharacteristics(instance.respectEffect, CharacteristicType.respect);
        performanceForCharacteristics(instance.knowledgeEffect, CharacteristicType.knowledge);
        performanceForCharacteristics(instance.healthEffect, CharacteristicType.health);
        performanceForCharacteristics(instance.moneyEffect, CharacteristicType.money);
    }

    /// <summary>
    /// Скрывает то, как будет влиять свайп карты на характеристики. (скрывает кружочки)
    /// </summary>
    public static void HideCardPerformanceForCharacteristics()
    {
        instance.respectEffect.HideEffect();
        instance.knowledgeEffect.HideEffect();
        instance.healthEffect.HideEffect();
        instance.moneyEffect.HideEffect();
    }

    /// <summary>
    /// Устанавливает день.
    /// </summary>
    public static void SetDay(int day)
    {
        instance.day.text = "День " +  day.ToString();
    }

    /// <summary>
    /// Устанавливает имя.
    /// </summary>
    public static void SetNamePlayer(string name)
    {
        instance.namePlayer.text = name;
    }

    /// <summary>
    /// Устанавливает характеристики.
    /// </summary>
    public static void SetCharacteristics(Characteristics characteristics)
    {
        instance.respectImage.fillAmount = (float)characteristics.respect / Characteristics.maxValue;
        instance.knowledgeImage.fillAmount = (float)characteristics.knowledge / Characteristics.maxValue;
        instance.healthImage.fillAmount = (float)characteristics.health / Characteristics.maxValue;
        instance.moneyImage.fillAmount = (float)characteristics.money / Characteristics.maxValue;
    }
        public void ShowAnimation(SwipeType swipeType, CharacteristicType characteristicType)
    {

    }
    /// <summary>
    /// Показывает анимацию роста или падения характеристик.
    /// </summary>
    public static void ShowAnimation(Card card, SwipeType swipeType)
    {
        void performanceForCharacteristics(int numberAnimation, string nameAnimation ,CharacteristicType type)
        {
            // Влияние на данную характеристику
            int effect = card.GetCharacteristic(swipeType,default, type);

            if (effect == 0)
            {
                return;
            }
                if (effect < 0)
                {
                    Debug.Log("работает?");
                    instance.characteristicsEffect[numberAnimation].Play($"{nameAnimation}EffectFall");
                }
                else if (effect > 0)
                {
                    instance.characteristicsEffect[numberAnimation].Play($"{nameAnimation}EffectGrow");
                }        
        }

        performanceForCharacteristics(0,"respect",CharacteristicType.respect);
        performanceForCharacteristics(1,"knowledge",CharacteristicType.knowledge);
        performanceForCharacteristics(2,"health",CharacteristicType.health);
        performanceForCharacteristics(3,"money",CharacteristicType.money);
    }
}
