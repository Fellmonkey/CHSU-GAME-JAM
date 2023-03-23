using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Контроллер карты, которая сейчас в игре.
/// </summary>
public class CardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Card card { get; private set; }

    // Средство управления текстом у карты.
    private TextOnCardController textController;

    [Header("Start card position")] // начальная позиция карты
    [SerializeField] private Vector3 startPosition;

    [Header("Swipe positions")] // позиции карты при свайпе
    [SerializeField] private Vector3 swipePositionRight;
    [SerializeField] private Vector3 swipePositionLeft;
    
    [Header("Text")]
    [SerializeField] private TMPro.TextMeshProUGUI titleText; // текст на карточке

    [Header("Sprites")]
    [SerializeField] private Sprite backgroundSprite; // задний фон у карты
    // стандартный передний фон (если не выбран)
    [SerializeField] private Sprite defaultForegroundSprite;

    [Header("Images")]
    [SerializeField] private Image cardImage; // Основное изображение на карте.
    [SerializeField] private Image bgImage; // Основное изображение на карте.

    [Header("Animator")]
    [SerializeField] private Animation animatorCard;

    [Header("Values")] // разные значения
    [SerializeField] private float swipingSpeed; // скорость при свайпе
    [SerializeField] private float returnSpeed; // скорость возврата к исходной позиции
    [SerializeField] private float dragSpeed; // Скорость следования карты за мышкой
    [SerializeField] private float rotationCoefficent; // коэф. вращения

    [Header("Maximum deviations")] // Максимальные отклонения карты (чтобы не ушла за экран)
    [SerializeField] private float maxDeviationRight;
    [SerializeField] private float maxDeviationLeft;
    [SerializeField] private float maxDeviationUp;
    [SerializeField] private float maxDeviationDown;

    [Header("Deviation for show text")] // отколнения, при которых уже возможен свайп.
    [SerializeField] private float deviationRight;
    [SerializeField] private float deviationLeft;


    private RectTransform rectTransform; // этого объекта
    private Vector3 startMousePos;

    private SwipeType swipeType; // тип свайпа

    [SerializeField] private bool isDragging; // перетаскиваем
    [SerializeField] private bool isSwiping; // карту свайпнули

    private bool checkSound; // флаг, который позволяет проигрывать звук один раз 

    /// <summary>
    /// Инициализация карты.
    /// </summary>
    public void InitCard(Card card)
    {
        this.card = card;

        rectTransform = GetComponent<RectTransform>();
        textController = GetComponent<TextOnCardController>();

        isDragging = false;
        isSwiping = false;

        rectTransform.localPosition = startPosition;
        bgImage.gameObject.SetActive(true);
        cardImage.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);

        textController.SetLeftText(card.swipe_left.text);
        textController.SetRightText(card.swipe_right.text);

        animatorCard.Play();
    }

    private void Update()
    {
        if(animatorCard.isPlaying) return; // если анимация играет скрипт не выполняется. Жесткая ошибка выключать скрипт (баги обеспечены ^_^).

        if (isDragging) // перетаскивание карты
        {
            OnDrag();
        }
        else if (isSwiping) // свайп карты
        {
            Vector3 v;

            if (swipeType == SwipeType.Right)
            {
                v = Vector3.Lerp(rectTransform.localPosition, swipePositionRight, swipingSpeed * Time.deltaTime);
            }
            else
            {
                v = Vector3.Lerp(rectTransform.localPosition, swipePositionLeft, swipingSpeed * Time.deltaTime);
            }

            rectTransform.localPosition = v;
        }
        else // возвращение на исходную позицию
        {
            Vector3 v = Vector3.Lerp(rectTransform.localPosition, startPosition, returnSpeed * Time.deltaTime);
            rectTransform.localPosition = v;
        }
        
        UpdateRotationCard();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSwiping) return;
        
        isDragging = true;
        startMousePos = Input.mousePosition;
    }

    public void OnDrag() // перетаксивание карты
    {
        Vector3 move = Input.mousePosition - startMousePos;
        Vector3 newPos = move + startPosition;
        newPos.z = startPosition.z;

        newPos.x = Mathf.Clamp(newPos.x, maxDeviationLeft, maxDeviationRight);
        newPos.y = Mathf.Clamp(newPos.y, maxDeviationDown, maxDeviationUp);

        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, newPos, dragSpeed * Time.deltaTime);

        // если перетащили карту вправо
        if (rectTransform.localPosition.x >= deviationRight)
        {
            if (card.swipe_right.text.Length > 0) // Если есть какой-то текст
                textController.ShowText(SwipeType.Right); // показать правый текст

            // что будет с хар-ками
            UIManager.ShowCardPerformanceForCharacteristics(card, SwipeType.Right); 

            if (checkSound)
            {
                checkSound = false;
                AudioManager.Instance.PlaySFX("clockRight"); // воспроизведение звука 
            }
        }
        // если перетащили карту влево
        else if (rectTransform.localPosition.x <= deviationLeft)
        {
            if (card.swipe_left.text.Length > 0) // Если есть какой-то текст
                textController.ShowText(SwipeType.Left); // показать левый текст
            
            // что будет с хар-ками
            UIManager.ShowCardPerformanceForCharacteristics(card, SwipeType.Left); 

            if (checkSound)
            {
                checkSound = false;
                AudioManager.Instance.PlaySFX("clockLeft"); // воспроизведение звука 
            }
        }
        else
        {
            textController.HideText();
            UIManager.HideCardPerformanceForCharacteristics();
            checkSound = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        textController.HideText();
        UIManager.HideCardPerformanceForCharacteristics();

        if (rectTransform.localPosition.x >= deviationRight) // свайп вправо
        {
            swipeType = SwipeType.Right;
            isSwiping = true;

            GameManager.SwipingCard(this, swipeType);
            UIManager.ShowAnimation(card, swipeType);
            AudioManager.Instance.PlaySFX("newCard");
        }
        else if (rectTransform.localPosition.x <= deviationLeft) // свайп влево
        {
            swipeType = SwipeType.Left;
            isSwiping = true;

            GameManager.SwipingCard(this, swipeType);
            UIManager.ShowAnimation(card, swipeType);
            AudioManager.Instance.PlaySFX("newCard");
        }
    }
    
    private void UpdateRotationCard()
    {
        Vector3 newAngle = new Vector3(0,0,(rectTransform.localPosition.x - startPosition.x) * rotationCoefficent * -1);
        rectTransform.eulerAngles = newAngle;
    }

    /// <summary>
    /// Для анимации. Устанавливает нужную картинку и текст.
    /// </summary>
    public void SetImageAndText()
    {
        // Карточка с текстом
        if ((card.swipe_left.text.Length == 0 && card.swipe_right.text.Length == 0) ||
            card.cardImageName == "")
        {
            titleText.text = card.title; // Устанавливаем сам текст
            titleText.gameObject.SetActive(true); // вкл. текст на карточке
            cardImage.gameObject.SetActive(false);

            cardImage.sprite = null; // убираем изображение с карты
            UIManager.SetTitle(""); // Убираем title текст
        }
        else // Обычная карточка
        {
            UIManager.SetTitle(card.title); // Ставим текст
            
            // Получаем картинку (если она вообще есть)
            Sprite sprite = ResourcesManager.GetSprite(card.cardImageName);

            if (sprite == null) // картинки нет
            {
                // устанавливаем дефолтую
                cardImage.sprite = defaultForegroundSprite;

                if (cardImage.sprite == null)
                {
                    cardImage.gameObject.SetActive(false);
                }
            }
            else
            {
                // устанавливаем нужную картинку
                cardImage.sprite = sprite;
            }

            cardImage.gameObject.SetActive(true);
        }

        if (card.speakingName.Length > 0) // Есть имя говорящего
        {
            UIManager.SetNameSpeaking(card.speakingName);
        }
        else // нет имени говорящего
        {
            UIManager.SetNameSpeaking("");
        }

        bgImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Удалить карту.
    /// </summary>
    /// <param name="time">Время, через которое карта удалится</param>
    public void DeleteCard(float time = 0)
    {
        Destroy(gameObject, time);
    }
}
