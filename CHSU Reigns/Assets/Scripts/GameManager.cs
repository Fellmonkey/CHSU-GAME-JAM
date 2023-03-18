using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game characteristics")]
    [SerializeField] private Characteristics gameCharacteristics;

    [Header("Card prefab")]
    public GameObject CardPrefab;

    [Header("Card holder")]
    public Transform CardHolder;


    private void Awake()
    {
        instance = this;
        CardsManager.LoadCards();
    }

    public void Start()
    {
        CreateNewCard();
    }

    public void SwipingCard(Card card, SwipeType swipeType, GameObject go)
    {
        Destroy(go, 2);
        Invoke("CreateNewCard", 0.5f);
    }

    public void CreateNewCard()
    {
        CardsManager.Put—ardIntoGame(CardsManager.GetRandomCard());
    }
}
