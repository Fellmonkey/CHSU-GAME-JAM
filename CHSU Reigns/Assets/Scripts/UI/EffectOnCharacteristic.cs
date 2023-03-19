

/// <summary>
/// Эффект на какую-либо характеристику. ( ружочек)
/// </summary>
[System.Serializable]
public class EffectOnCharacteristic
{
    public EffectOnCharacteristicController controller; // Контроллер
    public CharacteristicType characteristicType; // Тип характеристики (деньги, ум, уважение и т.д)
}
