
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
}
