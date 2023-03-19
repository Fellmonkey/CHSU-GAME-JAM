using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// ���������� �����, ������� ������ � ����.
/// </summary>
public class CardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Card card { get; private set; }

    // �������� ���������� ������� � �����.
    private TextOnCardController textController;

    [Header("Start card position")] // ��������� ������� �����
    [SerializeField] private Vector3 startPosition;

    [Header("Swipe positions")] // ������� ����� ��� ������
    [SerializeField] private Vector3 swipePositionRight;
    [SerializeField] private Vector3 swipePositionLeft;

    [Header("Values")] // ������ ��������
    [SerializeField] private float swipingSpeed; // �������� ��� ������
    [SerializeField] private float returnSpeed; // �������� �������� � �������� �������
    [SerializeField] private float dragSpeed; // �������� ���������� ����� �� ������
    [SerializeField] private float rotationCoefficent; // ����. ��������

    [Header("Maximum deviations")] // ������������ ���������� ����� (����� �� ���� �� �����)
    [SerializeField] private float maxDeviationRight;
    [SerializeField] private float maxDeviationLeft;
    [SerializeField] private float maxDeviationUp;
    [SerializeField] private float maxDeviationDown;

    [Header("Deviation for show text")] // ����������, ��� ������� ��� �������� �����.
    [SerializeField] private float deviationRight;
    [SerializeField] private float deviationLeft;


    private RectTransform rectTransform; // ����� �������
    private Vector3 startMousePos;

    private SwipeType swipeType; // ��� ������

    [SerializeField] private bool isDragging; // �������������
    [SerializeField] private bool isSwiping; // ����� ���������


    /// <summary>
    /// ������������� �����.
    /// </summary>
    /// <param name="card"></param>
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

        UIManager.instance.SetTitle(card.title); // ������ �����
        textController.SetLeftText(card.swipe_left.text);
        textController.SetRightText(card.swipe_right.text);
    }

    private void Update()
    {
        if (isDragging) // �������������� �����
        {
            OnDrag();
        }
        else if (isSwiping) // ����� �����
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
        else // ����������� �� �������� �������
        {
            Vector3 v = Vector3.Lerp(rectTransform.localPosition, startPosition, returnSpeed * Time.deltaTime);
            rectTransform.localPosition = v;
            UpdateRotationCard();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSwiping) return;
        
        isDragging = true;
        startMousePos = Input.mousePosition;
    }

    public void OnDrag()
    {
        Vector3 move = Input.mousePosition - startMousePos;
        Vector3 newPos = move + startPosition;
        newPos.z = startPosition.z;

        newPos.x = Mathf.Clamp(newPos.x, maxDeviationLeft, maxDeviationRight);
        newPos.y = Mathf.Clamp(newPos.y, maxDeviationDown, maxDeviationUp);

        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, newPos, dragSpeed * Time.deltaTime);

        // ���� ���������� ����� ������
        if (rectTransform.localPosition.x >= deviationRight)
        {
            textController.ShowText(SwipeType.Right); // �������� ������ �����
            UIManager.instance.ShowCardPerformanceForCharacteristics(card, SwipeType.Right); // ��� ����� � ���-����
        }
        // ���� ����� ����� �����
        else if (rectTransform.localPosition.x <= deviationLeft)
        {
            textController.ShowText(SwipeType.Left); // �������� ����� �����
            UIManager.instance.ShowCardPerformanceForCharacteristics(card, SwipeType.Left); // ��� ����� � ���-����
        }
        else
        {
            textController.HideText();
            UIManager.instance.HideCardPerformanceForCharacteristics();
        }
            
            
        UpdateRotationCard();
    }

    public void OnPointerUp(PointerEventData eventData)
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
    
    private void UpdateRotationCard()
    {
        Vector3 newAngle = new Vector3(0,0,(rectTransform.localPosition.x - startPosition.x) * rotationCoefficent * -1);
        rectTransform.eulerAngles = newAngle;
    }
}
