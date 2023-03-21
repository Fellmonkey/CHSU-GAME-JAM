
/// <summary>
/// Характеристики игры. (+карты)
/// </summary>
[System.Serializable]
public class Characteristics
{
    public int respect; // Репутация
    public int knowledge; // Знания
    public int health; // Здоровье
    public int money; // Деньги

    public const int maxValue = 16; // мин. значение
    public const int minValue = 0; // макс. значение

    public const int smallChange = 1; // маленькое изменение -> маленький кружочек
    public const int largeChange = 2; // значительное изменение -> большой кружочек

    public void AddCharacteristics(Characteristics characteristics,
        bool checkMinValue = false)
    {
        respect += characteristics.respect;
        knowledge += characteristics.knowledge;
        health += characteristics.health;
        money += characteristics.money;

        if (checkMinValue)
        {
            if (respect < minValue) respect = minValue;
            if (health < minValue) health = minValue;
            if (money < minValue) money = minValue;
            if (knowledge < minValue) knowledge = minValue;
        }
    }

    public void SetDefaultValues()
    {
        respect = maxValue / 2;
        knowledge = maxValue / 2;
        health = maxValue / 2;
        money = maxValue / 2;
    }
}
