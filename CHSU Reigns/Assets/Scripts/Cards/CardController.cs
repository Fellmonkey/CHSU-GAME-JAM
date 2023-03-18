using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Контроллер карты, которая сейчас в игре.
/// </summary>
public class CardController : MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler
{
    public Card card { get; private set; }

    // Средство управления текстом у карты.
    [SerializeField] private TextOnCardController textController;

    [Header("Start card position")] // начальная позиция карты
    [SerializeField] private Vector3 startPosition;

    [Header("Swipe positions")] // позиции карты при свайпе
    [SerializeField] private Vector3 swipePositionRight;
    [SerializeField] private Vector3 swipePositionLeft;

    [Header("Values")] // разные значения
    [SerializeField] private float swipingSpeed; // скорость при свайпе
    [SerializeField] private float returnSpeed; // скорость возврата к исходной позиции
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


    /// <summary>
    /// Инициализация карты.
    /// </summary>
    /// <param name="card"></param>
    public void InitCard(Card card)
    {
        this.card = card;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = startPosition;
        isDragging = false;
        isSwiping = false;

        UIManager.instance.SetTitle(card.title); // Ставим текст
        textController.SetLeftText(card.swipe_left.text);
        textController.SetRightText(card.swipe_right.text);
    }

    private void Update()
    {
        if (isSwiping)
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
            UpdateRotationCard();
        }
        else if (!isDragging)
        {
            Vector3 v = Vector3.Lerp(rectTransform.localPosition, startPosition, returnSpeed * Time.deltaTime);
            rectTransform.localPosition = v;
            UpdateRotationCard();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSwiping) return;
        
        isDragging = true;
        startMousePos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSwiping) return;

        Vector3 move = Input.mousePosition - startMousePos;
        Vector3 newPos = move + startPosition;
        newPos.z = startPosition.z;

        newPos.x = Mathf.Clamp(newPos.x, maxDeviationLeft, maxDeviationRight);
        newPos.y = Mathf.Clamp(newPos.y, maxDeviationDown, maxDeviationUp);

        rectTransform.localPosition = newPos;

        // если перетащили карту вправо
        if (rectTransform.localPosition.x >= deviationRight)
        {
            textController.ShowText(SwipeType.Right); // показать правый текст
            UIManager.instance.ShowCardPerformanceForCharacteristics(card, SwipeType.Right); // что будет с хар-ками
        }
        // если карту карту влево
        else if (rectTransform.localPosition.x <= deviationLeft)
        {
            textController.ShowText(SwipeType.Left); // показать левый текст
            UIManager.instance.ShowCardPerformanceForCharacteristics(card, SwipeType.Left); // что будет с хар-ками
        }
        else
        {
            textController.HideText();
            UIManager.instance.HideCardPerformanceForCharacteristics();
        }
            
            
        UpdateRotationCard();
    }

    private void UpdateRotationCard()
    {
        Vector3 newAngle = new Vector3(0,0,(rectTransform.localPosition.x - startPosition.x) * rotationCoefficent * -1);
        rectTransform.eulerAngles = newAngle;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        textController.HideText();
        UIManager.instance.HideCardPerformanceForCharacteristics();

        if (rectTransform.localPosition.x >= deviationRight)
        {
            swipeType = SwipeType.Right;
            isSwiping = true;

            GameManager.instance.SwipingCard(card, swipeType, gameObject);
        }
        else if (rectTransform.localPosition.x <= deviationLeft)
        {
            swipeType = SwipeType.Left;
            isSwiping = true;

            GameManager.instance.SwipingCard(card, swipeType, gameObject);
        }
    }
}
