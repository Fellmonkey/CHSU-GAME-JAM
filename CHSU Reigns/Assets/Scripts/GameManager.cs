using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //KINGDOM
    public static int kingdomSafety = 20;
    public static int kingdomFaith = 20;
    public static int kingdomTreasure = 20;
    public static int kingdomPeople = 20;
    public static int kingdomArmy = 20;
    public static int maxValue = 100;
    public static int minValue = 0;
    
    [SerializeField] private GameObject cardGameObject;
    [SerializeField] private SpriteRenderer cardSpriteRenderer;
    public ResourceManager resourceManager;
    public CardController mainCardController;
    public UIManager uIManager;
    public Vector2  defaultPositionCard;
    //Tweaking variables
    public float movingSpeed;
    [Range(0,5)]
    public float sideMargin;
    public float sideTrigger;
    public float divideValue;
    public float backgroundDivideValue;
    public float rotationCoefficent;
    public Color textColor;
    public Color actionBackgroundColor;
    [Range(0,1)]
    public float transparency;
    //UI
    public TMP_Text display;
    public TMP_Text characterDialogue;
    public TMP_Text chooseDialogue;
    public SpriteRenderer actionBackground;

    //Card variables
    public string direction;
    private string leftQuote;
    private string rightQuote;
    public Card currentCard;
    public Card testCard;
    //Substituting the card
    public bool isSubstituting = false;
    public Vector3 cardRotation;
    public Vector3 initRotation;
    public SpriteRenderer cardSprite;
    public Sprite cardBack;
    public Sprite cardFront;


    void Start()
    {
        LoadCard(testCard);
    }
    void UpdateDialogue()
    {
        chooseDialogue.color = textColor;
        actionBackground.color = actionBackgroundColor;
        if(cardGameObject.transform.position.x < 0)
        {
            chooseDialogue.text = leftQuote;

        }
        else
        {
            chooseDialogue.text = rightQuote;
        }
    }
    void Movement()
    {
         // Movement
        if(Input.GetMouseButton(0) && mainCardController.IsMouseOver)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cardGameObject.transform.position = pos;
            cardGameObject.transform.eulerAngles = new Vector3(0, 0, cardGameObject.transform.position.x * rotationCoefficent);
        }
        else if (!isSubstituting)
        {
            cardGameObject.transform.position = Vector2.MoveTowards(cardGameObject.transform.position,defaultPositionCard, movingSpeed);
            cardGameObject.transform.eulerAngles = Vector3.zero;
        }
        else if (isSubstituting)
        {      
            cardGameObject.transform.eulerAngles = Vector3.MoveTowards(cardGameObject.transform.eulerAngles, cardRotation, 0.5f);
            if (cardGameObject.transform.eulerAngles.y >= 90)
            {
                cardSprite.sprite = cardBack;
            } 
            else
            {
                cardSprite.sprite = cardFront;
            }
        }
    }
    void RotatingCard()
    {
        if(cardGameObject.transform.eulerAngles == cardRotation)
        {
            isSubstituting = false;
        }
    }
    void KingdomValuesLogic()
    {
        Debug.Log(kingdomArmy);
    }
    void UpdateTransparency()
    {
         //Dialogue text handing
        textColor.a =  Mathf.Min((Mathf.Abs(cardGameObject.transform.position.x) - sideMargin) / divideValue, 1);
        actionBackgroundColor.a =  Mathf.Min((Mathf.Abs(cardGameObject.transform.position.x) - sideMargin) / backgroundDivideValue, transparency);

        if(cardGameObject.transform.position.x > sideTrigger)
        {
            direction = "right";
            if(Input.GetMouseButtonUp(0))
            {
                uIManager.AnimationLogic();
                currentCard.Right();
                NewCard();
            }          
        }
        else if(cardGameObject.transform.position.x > sideMargin)
        {
            direction = "right";           
        }
        else if(cardGameObject.transform.position.x > - sideMargin)
        {
            direction = "none";
            textColor.a = 0;        
        }
        else if(cardGameObject.transform.position.x > - sideTrigger)
        {
            direction = "left";
        }
        else
        {
            direction = "left";
            if(Input.GetMouseButtonUp(0))
            {
                uIManager.AnimationLogic();
                currentCard.Left();
                NewCard();
            }  
        }
    }
    void Update()
    {
        uIManager.UpdateUI();
        UpdateTransparency();
        UpdateDialogue();
        Movement();
        RotatingCard();

        display.text = textColor.a.ToString();
    }

    public void LoadCard(Card card)
    {
        cardSpriteRenderer.sprite = resourceManager.sprites[(int)card.sprite];
        characterDialogue.text = card.dialogue;
        leftQuote = card.leftQuote;
        rightQuote = card.rightQuote;
        currentCard = card;

        //Reseting the position of the card
        cardGameObject.transform.position = defaultPositionCard;
        cardGameObject.transform.eulerAngles = Vector3.zero; 
        //Initialize of the substitution
        isSubstituting = true;
         cardGameObject.transform.eulerAngles = initRotation;
    }
    public void NewCard()
    {
        int rollDice = Random.Range(0, resourceManager.cards.Length);
        LoadCard(resourceManager.cards[rollDice]);

    }

}
