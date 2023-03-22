using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


/// <summary>
/// Менеджер ресурсов.
/// </summary>
public static class ResourcesManager
{
    private static List<NamedSprite> sprites = new List<NamedSprite>();

    /// <summary>
    /// Загружает карты из папки Resources.
    /// </summary>
    public static Card[] GetCards()
    {
        TextAsset[] textsAssets = Resources.LoadAll<TextAsset>("Cards");
        List<Card> cards = new List<Card>();

        for (int i = 0; i < textsAssets.Length; i++)
        {
            Card card = new Card();
            card = JsonUtility.FromJson<Card>(textsAssets[i].text);
            cards.Add(card);
        }

        return cards.ToArray();
    }


    /// <summary>
    /// Возвращает спрайт из папки Resources (или null).
    /// </summary>
    public static Sprite GetSprite(string name)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (sprites[i].name == name)
                return sprites[i].sprite;
        }

        Sprite sprite;

        try
        {
            sprite = Resources.Load<Sprite>("Sprites/" + name);

            if (sprite != null)
            {
                sprites.Add(new NamedSprite(name, sprite));
            }

            return sprite;
        }
        catch (Exception) { }


        return null;
    }

    private class NamedSprite
    {
        public string name;
        public Sprite sprite;

        public NamedSprite(string name, Sprite sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }
}
