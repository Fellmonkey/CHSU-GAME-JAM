using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int cardID;
    public string cardName;
    public CardSprite sprite;
    public string dialogue;
    public string leftQuote;
    public string rightQuote;

    //left
    public int kSafetyL;
    public int kFaithL;
    public int kTreasureL;
    public int kPeopleL;
    public int kArmyL;

    //right
    public int kSafetyR;
    public int kFaithR;
    public int kTreasureR;
    public int kPeopleR;
    public int kArmyR;
    public void Left()
    {
        Debug.Log(cardName + " swiped left"); 
        GameManager.kingdomArmy += kArmyL;
        GameManager.kingdomFaith += kFaithL;
        GameManager.kingdomPeople += kPeopleL;
        GameManager.kingdomSafety += kSafetyL;
        GameManager.kingdomTreasure += kTreasureL;
    }
    public void Right()
    {
        Debug.Log(cardName + " swiped right"); 
        GameManager.kingdomArmy += kArmyR;
        GameManager.kingdomFaith += kFaithR;
        GameManager.kingdomPeople += kPeopleR;
        GameManager.kingdomSafety += kSafetyR;
        GameManager.kingdomTreasure += kTreasureR;
    }
}
