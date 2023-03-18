using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Контроллер карты, которая сейчас в игре.
/// </summary>
public class CardController : MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler
{
    public Card card;

    [SerializeField] private TextOnCardController textOnCardController;

    [Header("Start card position")]
    [SerializeField] private Vector3 startPosition;

    [Header("Swipe positions")]
    [SerializeField] private Vector3 swipePositionRight;
    [SerializeField] private Vector3 swipePositionLeft;

    [Header("Values")]
    [SerializeField] private float swipingSpeed;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float rotationCoefficent;

    [Header("Maximum deviations")] // Максимальные отклонения карты (чтобы не ушла за экран)
    [SerializeField] private float maxDeviationRight;
    [SerializeField] private float maxDeviationLeft;
    [SerializeField] private float maxDeviationUp;
    [SerializeField] private float maxDeviationDown;

    [Header("Deviation for show text")]
    [SerializeField] private float deviationRight;
    [SerializeField] private float deviationLeft;


    private RectTransform rectTransform;
    private Vector3 startMousePos;

    private SwipeType swipeType;

    [SerializeField] private bool isDragging; // перетаскиваем
    [SerializeField] private bool isSwiping; // карту свайпнули

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

        if (rectTransform.localPosition.x >= deviationRight)
            textOnCardController.ShowText(SwipeType.Right); 
        else if (rectTransform.localPosition.x <= deviationLeft)
            textOnCardController.ShowText(SwipeType.Left);
        else
            textOnCardController.HideText();
            
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
        textOnCardController.HideText();

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
