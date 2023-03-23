using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static readonly string pathToSaveGame;

    static SaveManager()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        pathToSaveFolder = Path.Combine(Application.persistentDataPath, "Saved games", "Save.json");
#else
        pathToSaveGame = Path.Combine(Application.dataPath, "Saved games", "Save.json");
#endif
    }

    /// <summary>
    /// ��������� ����
    /// </summary>
    public static void SaveGame(SaveGame saveGame)
    {
        string json = JsonUtility.ToJson(saveGame);

        if (!Directory.Exists(Path.GetDirectoryName(pathToSaveGame)))
            Directory.CreateDirectory(Path.GetDirectoryName(pathToSaveGame));

        File.WriteAllText(pathToSaveGame, json);
    }

    /// <summary>
    /// ���������, ���� �� ����������� ����
    /// </summary>
    public static bool CheckSavedGames()
    {
        return File.Exists(pathToSaveGame);
    }

    /// <summary>
    /// ��������� ����
    /// </summary>
    public static SaveGame LoadGame()
    {
        if (!CheckSavedGames()) return null;

        string json = File.ReadAllText(pathToSaveGame);

        SaveGame saveGame = JsonUtility.FromJson<SaveGame>(json);

        return saveGame;
    }
    /// <summary>
    /// Временное решение
    /// </summary>
    public static void DeleteSaveGame()
    {
        File.Delete(pathToSaveGame);
    }
}
    

/// <summary>
/// ���������� ����.
/// </summary>
[System.Serializable]
public class SaveGame
{
    // public string namePlayer;
    public PlayerClass playerClass;
    public int day;
    public int cardId;
    public Characteristics characteristics;

    public SaveGame(PlayerClass playerClass, 
        int day, int cardId, Characteristics characteristics)
    {
        //this.namePlayer = namePlayer;
        this.playerClass = playerClass;
        this.day = day;
        this.cardId = cardId;
        this.characteristics = characteristics;
    }
}

