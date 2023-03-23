using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Game characteristics")] 
    [SerializeField] private Characteristics gameCharacteristics; // хар-ки
    [SerializeField] private PlayerClass playerClass; // Класс игрока
    [SerializeField] private int gameDay; // Игровой день.

    [Header("Card prefab")]
    [SerializeField] private GameObject CardPrefab;

    [Header("Card holder")]
    [SerializeField] private Transform CardHolder;
    private int randomSessia;

    // Тип свайпа последней карты
    private SwipeType swipeType;

    private bool defeat; // поражение

    private void Awake()
    {
        instance = this;
        CardsManager.LoadCards();
        randomSessia = Random.Range(30,40);

    }

    private void Update() {

         if(gameDay >= randomSessia) SceneManager.LoadScene(1);
    }
    private void Start()
    {
        gameCharacteristics = new Characteristics();
        defeat = false;

        if (SaveManager.CheckSavedGames()) // Есть сохраненная игра
        {
            SaveGame saveGame = SaveManager.LoadGame();

            gameCharacteristics = saveGame.characteristics;
            gameDay = saveGame.day;
            playerClass = saveGame.playerClass;
            CreateNextCard(saveGame.cardId);
        }
        else
        {
            gameDay = 1;
            playerClass = (PlayerClass)Random.Range(0,3);
            gameCharacteristics.SetDefaultValues();
            CreateNextCard();
        }

        UIManager.SetPlayerClassUI(playerClass);
        UIManager.SetCharacteristics(gameCharacteristics);
        UIManager.SetDay(gameDay);

        AudioManager.Instance.PlayVoice("voice_1"); // воспроизведение звука бла-бла-бла
    }

    /// <summary>
    /// Свайп карты.
    /// </summary>
    public static void SwipingCard(CardController controller, SwipeType swipe)
    {
        // если проиграли и уже свайпнули карту проигрыша
        if (instance.defeat)
        {
            SaveManager.DeleteSaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        Card card = controller.card;

        // прибавляем характеристики
        instance.gameCharacteristics.AddCharacteristics(
            card.GetCharacteristic(swipe, instance.playerClass), true);

        // показываем
        UIManager.SetCharacteristics(instance.gameCharacteristics);

        // свайп
        instance.swipeType = swipe;

        // чекаем поражение
        if (instance.gameCharacteristics.respect == 0 || instance.gameCharacteristics.health == 0
            || instance.gameCharacteristics.knowledge == 0 || instance.gameCharacteristics.money == 0)
        {
            instance.defeat = true;
        }
        // еще не проиграли
        else
        {
            if (swipe == SwipeType.Left)
            {
                if (card.swipe_left.next.Length == 0) // нет след. карты -> + день
                {
                    instance.gameDay++;
                    UIManager.SetDay(instance.gameDay);
                }
                // иначе создастся некст карта
            }
            else
            {
                if (card.swipe_right.next.Length == 0) // нет след. карты -> + день
                {
                    instance.gameDay++;
                    UIManager.SetDay(instance.gameDay);
                }
                // иначе создастся некст карта
            }
        }

        instance.Invoke("CreateNextCard", 0.35f); // Создаем след. карту
        controller.DeleteCard(2f); // через 2 сек удаляем карту
    }

    /// <summary>
    /// Создание новой карты на игровом поле.
    /// </summary>
    private void CreateNextCard()
    {
        CardsManager.PutNextCardIntoGame();
        SaveGame();
    }

    /// <summary>
    /// Создание новой карты на игровом поле (по id).
    /// </summary>
    private void CreateNextCard(int id)
    {
        CardsManager.PutNextCardIntoGame(id);
        SaveGame();
    }

    private void SaveGame()
    {
        if (CardsManager.gameCard != null)
        {
            SaveManager.SaveGame(
                new SaveGame(playerClass, gameDay, CardsManager.gameCard.id, gameCharacteristics)
            );
        }
    }

    /// <summary>
    /// Возвращает харакеристики игры.
    /// </summary>
    public static Characteristics GetCharacteristics()
    {
        return instance.gameCharacteristics;
    }

    /// <summary>
    /// Возвращает класс игрока.
    /// </summary>
    public static PlayerClass GetPlayerClass()
    {
        return instance.playerClass;
    }

    /// <summary>
    /// Возвращает игровой день.
    /// </summary>
    public static int GetGameDay()
    {
        return instance.gameDay;
    }

    public static GameObject GetCardPrefab()
    {
        return instance.CardPrefab;
    }

    public static Transform GetCardHolder()
    {
        return instance.CardHolder;
    }

    public static SwipeType GetSwipeLastCard()
    {
        return instance.swipeType;
    }

    public static bool GetDefeat()
    {
        return instance.defeat;
    }
}
