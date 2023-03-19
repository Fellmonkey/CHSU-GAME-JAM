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
    /// <returns></returns>
    public static Card GetRandomCard()
    {
        int rand = UnityEngine.Random.Range(0, randomCards.Length);
        return randomCards[rand];
    }

    /// <summary>
    /// Ищет карту, которая связана с полученным днем.
    /// </summary>
    /// <returns>Возвращает эту карту, если она найдена, иначе null.</returns>
    public static Card CheckTheDayCard(int day)
    {
        for (int i = 0; i < eventCards.Length; i++)
        {
            if (eventCards[i].day == day)
                return eventCards[i];
        }

        return null;
    }

    /// <summary>
    /// Вводит полученную карту в игру.
    /// </summary>
    /// <param name="card"></param>
    public static void PutCardIntoGame(Card card, GameObject cardPrefab, Transform cardHolder)
    {
        GameObject go = Object.Instantiate(cardPrefab, Vector3.zero, 
            Quaternion.identity, cardHolder);
        go.GetComponent<CardController>().InitCard(card);
        gameCard = card;
    }
}

