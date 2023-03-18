
/// <summary>
/// ������� �����.
/// </summary>
[System.Serializable]
public class Card
{
    private static int _id = 0;
    public readonly int id; // �� 

    public string title; // ���������
    public int day; // ����
    public string backgroundImageName; // ������ ���
    public string cardImageName; // ����������� �� �����
    public Swipe swipe_left; // ����� �����
    public Swipe swipe_right; // ����� ������


    public Card()
    {
        id = _id++;
        swipe_left = new Swipe();
        swipe_right = new Swipe();
    }

    /// <summary>
    /// ���������� ��� �������������� �� ������ � ������ ������.
    /// </summary>
    public Characteristics GetCharacteristic(SwipeType swipeType, PlayerClass playerClass)
    {
        if (swipeType == SwipeType.Left)
            return swipe_left.GetCharacteristics(playerClass);
        else
            return swipe_right.GetCharacteristics(playerClass);
    }


    /// <summary>
    /// ���������� ����������� �������������� �� ������ � ������ ������.
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
    /// ����� �����. (������� �� ������)
    /// </summary>
    [System.Serializable]
    public class Swipe
    {
        public string text; // ����� ��� ������
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
        /// ���������� �������������� ��� ������������� ������.
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

