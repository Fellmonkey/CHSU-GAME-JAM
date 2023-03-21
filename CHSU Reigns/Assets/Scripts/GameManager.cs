using UnityEngine;


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


    private void Awake()
    {
        instance = this;
        CardsManager.LoadCards();
    }

    private void Start()
    {
        gameCharacteristics = new Characteristics();

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
            gameCharacteristics.SetDefaultValues();
            CreateNextCard();
        }

        UIManager.SetCharacteristics(gameCharacteristics);
        UIManager.SetDay(gameDay);

        AudioManager.Instance.PlayVoice("voice_1"); // воспроизведение звука бла-бла-бла
    }

    /// <summary>
    /// Свайп карты.
    /// </summary>
    public static void SwipingCard(CardController controller, SwipeType swipe)
    {
        Card card = controller.card;

        instance.gameCharacteristics.AddCharacteristics(
            card.GetCharacteristic(swipe, instance.playerClass), true);

        UIManager.SetCharacteristics(instance.gameCharacteristics);

        if (card.next.Length == 0) // нет след. карты -> + день
        {
            instance.gameDay++;
            UIManager.SetDay(instance.gameDay);
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
}
