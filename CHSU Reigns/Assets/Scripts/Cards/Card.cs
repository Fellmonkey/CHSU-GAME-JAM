
/// <summary>
/// Игровая карта.
/// </summary>
[System.Serializable]
public class Card
{
    private static int _id = 0;
    public readonly int id; // ид 

    public string title; // заголовок
    public int day; // день
    public string backgroundImageName; // задний фон
    public string cardImageName; // изображение на карте
    public Swipe swipe_left; // свайп влево
    public Swipe swipe_right; // свайп вправо


    public Card()
    {
        id = _id++;
        swipe_left = new Swipe();
        swipe_right = new Swipe();
    }

    /// <summary>
    /// Возвращает всю характеристику по свайпу и классу игрока.
    /// </summary>
    public Characteristics GetCharacteristic(SwipeType swipeType, PlayerClass playerClass)
    {
        if (swipeType == SwipeType.Left)
            return swipe_left.GetCharacteristics(playerClass);
        else
            return swipe_right.GetCharacteristics(playerClass);
    }


    /// <summary>
    /// Возвращает определённую характеристику по свайпу и классу игрока.
    /// </summary>
    public int GetCharacteristic(SwipeType swipeType, PlayerClass playerClass, CharacteristicType characteristicType)
    {
        Characteristics characteristics = GetCharacteristic(swipeType, playerClass);

        if (characteristicType == CharacteristicType.respect)
            return characteristics.respect;
        else if (characteristicType == CharacteristicType.health)
            return characteristics.health;
        else if (characteristicType == CharacteristicType.money)
            return characteristics.money;
        else
            return characteristics.knowledge;
    }
    
    
    /// <summary>
    /// Свайп карты. (влияние на классы)
    /// </summary>
    [System.Serializable]
    public class Swipe
    {
        public string text; // текст при свайпе
        public Characteristics genius;
        public Characteristics psychologist;
        public Characteristics sportsman;

        public Swipe()
        {
            genius = new Characteristics();
            psychologist = new Characteristics();
            sportsman = new Characteristics();
        }


        /// <summary>
        /// Возвращает характеристику для определенного класса.
        /// </summary>
        public Characteristics GetCharacteristics(PlayerClass playerClass)
        {
            if (playerClass == PlayerClass.Genius)
                return genius;
            else if (playerClass == PlayerClass.Psychologist)
                return psychologist;
            else
                return sportsman;
        }
    }
}

