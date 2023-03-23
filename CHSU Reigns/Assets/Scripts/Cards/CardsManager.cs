using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Менеджер всех игровых карт.
/// </summary>
public static class CardsManager
{
    /// <summary>
    /// Карта, которая сейчас в игре.
    /// </summary>
    public static Card gameCard { private set; get; }

    /// <summary>
    /// Все игровые карты.
    /// </summary>
    public static Card[] cards { private set; get; }

    /// <summary>
    /// Карты оперделённых дней.
    /// </summary>
    public static Card[] eventCards { private set; get; }

    /// <summary>
    /// Случайные карты.
    /// </summary>
    public static Card[] randomCards { private set; get; }

    public static Card[] defeatMoneyCards { private set; get; }
    public static Card[] defeatHealthCards { private set; get; }
    public static Card[] defeatKnowledgeCards { private set; get; }
    public static Card[] defeatRespectCards { private set; get; }


    /// <summary>
    /// Загружает карты.
    /// </summary>
    public static void LoadCards()
    {
        cards = ResourcesManager.GetGameCards();
        List<Card> eventCards = new List<Card>();
        List<Card> randomCards = new List<Card>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].day == 0)
                randomCards.Add(cards[i]);
            else
                eventCards.Add(cards[i]);
        }

        CardsManager.eventCards = eventCards.ToArray();
        CardsManager.randomCards = randomCards.ToArray();

        defeatHealthCards = ResourcesManager.GetDefeatCards(CharacteristicType.health);
        defeatKnowledgeCards = ResourcesManager.GetDefeatCards(CharacteristicType.knowledge);
        defeatMoneyCards = ResourcesManager.GetDefeatCards(CharacteristicType.money);
        defeatRespectCards = ResourcesManager.GetDefeatCards(CharacteristicType.respect);
    }

    /// <summary>
    /// Возвращает случайную карту (не дневную).
    /// </summary>
    private static Card GetRandomCard()
    {
        int rand = UnityEngine.Random.Range(0, randomCards.Length);

        while (randomCards[rand].id == gameCard.id)
        {
            rand = UnityEngine.Random.Range(0, randomCards.Length);
        }

        return randomCards[rand];
    }

    /// <summary>
    /// Возвращает карту по имени.
    /// </summary>
    private static Card GetCard(string name)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].name == name)
                return cards[i];
        }

        return null;
    }

    /// <summary>
    /// Ищет карту, которая связана с полученным днем.
    /// </summary>
    /// <returns>Возвращает эту карту, если она найдена, иначе null.</returns>
    private static Card CheckTheDayCard(int day)
    {
        List<Card> eCards = new List<Card>(); // все карты относящиеся к данному дню

        for (int i = 0; i < eventCards.Length; i++)
        {
            if (eventCards[i].day == day)
                eCards.Add(cards[i]);
        }

        // Ищем первую карту данного дня.
        
        for (int i = 0; i < eCards.Count; i++)
        {
            if (eCards[i].firstDayCard)
            {
                return eCards[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Вводит следующую карту в игру.
    /// </summary>
    public static void PutNextCardIntoGame()
    {
        Card nextCard = null;
        SwipeType swipeType = GameManager.GetSwipeLastCard(); 

        if(GameManager.GetDefeat()) // поражение
        {
            Characteristics characteristics = GameManager.GetCharacteristics();

            if (characteristics.respect == 0)
            {
                nextCard = defeatRespectCards[Random.Range(0, defeatRespectCards.Length)];
            }
            else if (characteristics.knowledge == 0)
            {
                nextCard = defeatKnowledgeCards[Random.Range(0, defeatKnowledgeCards.Length)];
            }
            else if (characteristics.money == 0)
            {
                nextCard = defeatMoneyCards[Random.Range(0, defeatMoneyCards.Length)];
            }
            else
            {
                nextCard = defeatHealthCards[Random.Range(0, defeatHealthCards.Length)];
            }
        }
        else if(gameCard != null) // чек след. карты
        {
            // есть карта, при свайпе влево
            if (swipeType == SwipeType.Left && gameCard.swipe_left.next.Length > 0)
            {
                nextCard = GetCard(gameCard.swipe_left.next);
            }
            // вправо
            else if(swipeType == SwipeType.Right && gameCard.swipe_right.next.Length > 0)
            {
                nextCard = GetCard(gameCard.swipe_right.next);
            }
            // нет след. карт
            else
            {
                nextCard = CheckTheDayCard(GameManager.GetGameDay()); // ищем дневную

                if (nextCard == null) // если дневной нет, то берем случайную
                {
                    nextCard = GetRandomCard();
                }
            }
        }
        else // новый день
        {
            nextCard = CheckTheDayCard(GameManager.GetGameDay()); // ищем дневную

            if (nextCard == null) // если дневной нет, то берем случайную
            {
                nextCard = GetRandomCard();
            }
        }

        GameObject go = Object.Instantiate(GameManager.GetCardPrefab(), Vector3.zero, 
            Quaternion.identity, GameManager.GetCardHolder());
        go.GetComponent<CardController>().InitCard(nextCard);
        gameCard = nextCard;
    }

    /// <summary>
    /// Вводит карту в игру (по id).
    /// </summary>
    public static void PutNextCardIntoGame(int id)
    {
        Card card = null;

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].id == id)
            {
                card = cards[i];
                break;
            }
        }

        if (card == null)
        {
            PutNextCardIntoGame();
            return;
        }

        GameObject go = Object.Instantiate(GameManager.GetCardPrefab(), Vector3.zero,
            Quaternion.identity, GameManager.GetCardHolder());
        go.GetComponent<CardController>().InitCard(card);
        gameCard = card;
    }
}

