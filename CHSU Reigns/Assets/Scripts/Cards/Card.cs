
/// <summary>
/// Игровая карта.
/// </summary>
[System.Serializable]
public class Card
{
    private static int _id = 0;
    public int id { get; }

    public string title;
    public int day;
    public string backgroundImageName;
    public string cardImageName;
    public Swipe swipe_left;
    public Swipe swipe_right;

    /// <summary>
    /// Свайп карты.
    /// </summary>
    [System.Serializable]
    public class Swipe
    {
        public string text;
        public Characteristics genius;
        public Characteristics psychologist;
        public Characteristics sportsman;

        public Swipe()
        {
            genius = new Characteristics();
            psychologist = new Characteristics();
            sportsman = new Characteristics();
        }

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

    public Card()
    {
        id = _id++;
        swipe_left = new Swipe();
        swipe_right = new Swipe();
    }


    /// <summary>
    /// Возвращает характеристику по свайпу и классу игрока.
    /// </summary>
    /// <param name="swipeType"></param>
    /// <param name="playerClass"></param>
    /// <returns></returns>
    public Characteristics GetCharacteristics(SwipeType swipeType, PlayerClass playerClass)
    {
        if (swipeType == SwipeType.Left)
            return swipe_left.GetCharacteristics(playerClass);
        else
            return swipe_right.GetCharacteristics(playerClass);
    }
}

