using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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

    private bool checkSound; // флаг 

    /// <summary>
    /// Инициализация карты.
    /// </summary>
    public void InitCard(Card card)
    {
        this.card = card;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textController = GetComponent<TextOnCardController>();

        rectTransform.localPosition = startPosition;
        isDragging = false;
        isSwiping = false;

        UIManager.SetTitle(card.title); // Ставим текст
        textController.SetLeftText(card.swipe_left.text);
        textController.SetRightText(card.swipe_right.text);
    }

    private void Update()
    {
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
            textController.ShowText(SwipeType.Right); // показать правый текст
            UIManager.ShowCardPerformanceForCharacteristics(card, SwipeType.Right); // что будет с хар-ками

            if (checkSound)
            {
                checkSound = false;
                AudioManager.Instance.PlaySFX("clockRight"); // воспроизведение звука 
            }
        }
        // если карту карту влево
        else if (rectTransform.localPosition.x <= deviationLeft)
        {
            textController.ShowText(SwipeType.Left); // показать левый текст
            UIManager.ShowCardPerformanceForCharacteristics(card, SwipeType.Left); // что будет с хар-ками

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

        if (rectTransform.localPosition.x >= deviationRight)
        {
            swipeType = SwipeType.Right;
            isSwiping = true;

            GameManager.SwipingCard(this, swipeType);
        }
        else if (rectTransform.localPosition.x <= deviationLeft)
        {
            swipeType = SwipeType.Left;
            isSwiping = true;

            GameManager.SwipingCard(this, swipeType);
        }
    }
    
    private void UpdateRotationCard()
    {
        Vector3 newAngle = new Vector3(0,0,(rectTransform.localPosition.x - startPosition.x) * rotationCoefficent * -1);
        rectTransform.eulerAngles = newAngle;
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
