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

    /// <summary>
    /// Загружает карты.
    /// </summary>
    public static void LoadCards()
    {
        cards = ResourcesManager.GetCards();
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
    }

    /// <summary>
    /// Возвращает случайную карту (не дневную).
    /// </summary>
    private static Card GetRandomCard()
    {
        int rand = UnityEngine.Random.Range(0, randomCards.Length);
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
        Card nextCard;

        if(gameCard != null && gameCard.next.Length > 0) // след. карта
        {
            nextCard = GetCard(gameCard.next);
        }
        else
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
}

