using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �������� ���� ������� ����.
/// </summary>
public static class CardsManager
{

    /// <summary>
    /// �����, ������� ������ � ����.
    /// </summary>
    public static Card gameCard { private set; get; }

    /// <summary>
    /// ��� ������� �����.
    /// </summary>
    public static Card[] cards { private set; get; }

    /// <summary>
    /// ����� ����������� ����.
    /// </summary>
    public static Card[] eventCards { private set; get; }

    /// <summary>
    /// ��������� �����.
    /// </summary>
    public static Card[] randomCards { private set; get; }


    /// <summary>
    /// ��������� �����.
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
    /// ���������� ��������� ����� (�� �������).
    /// </summary>
    /// <returns></returns>
    public static Card GetRandomCard()
    {
        int rand = UnityEngine.Random.Range(0, randomCards.Length);
        return randomCards[rand];
    }

    /// <summary>
    /// ���� �����, ������� ������� � ���������� ����.
    /// </summary>
    /// <returns>���������� ��� �����, ���� ��� �������, ����� null.</returns>
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
    /// ������ ���������� ����� � ����.
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

