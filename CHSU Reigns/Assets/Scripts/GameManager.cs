using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using static EffectOnCharacteristic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Game characteristics
    public Characteristics gameCharacteristics { get; private set; }
    public PlayerClass playerClass { get; private set; }
    public int gameDay { get; private set; }


    [Header("Card prefab")]
    [SerializeField] private GameObject CardPrefab;

    [Header("Card holder")]
    [SerializeField] private Transform CardHolder;


    private void Awake()
    {
        instance = this;
        CardsManager.LoadCards();
    }

    public void Start()
    {
        CreateNewCard();
    }


    /// <summary>
    /// Свайп карты.
    /// </summary>
    public void SwipingCard(Card card, SwipeType swipeType, GameObject go)
    {
        Destroy(go, 2);
        Invoke("CreateNewCard", 0.5f);
    }


    /// <summary>
    /// Создание новой карты на игровом поле.
    /// </summary>
    public void CreateNewCard()
    {
        CardsManager.PutСardIntoGame(CardsManager.GetRandomCard(), CardPrefab, CardHolder);
    }
}
