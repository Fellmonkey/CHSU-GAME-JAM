using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Card
    public GameManager gameManager;
    public GameObject card;
    //UI icons
    public Image kingdomSafety;
    public Image kingdomFaith;
    public Image kingdomTreasure;
    public Image kingdomPeople;
    public Image kingdomArmy;

    //UI impact icons
    public Image kingdomSafetyImpact;
    public Image kingdomFaithImpact;
    public Image kingdomTreasureImpact;
    public Image kingdomPeopleImpact;
    public Image kingdomArmyImpact;

    //UI Animation
    public Animator kingdomSafetyAnimator;
    public Animator kingdomFaithAnimator;
    public Animator kingdomTreasureAnimator;
    public Animator kingdomPeopleAnimator;
    public Animator kingdomArmyAnimator;

    public void UpdateUI()
    {
        //UI icons
        kingdomSafety.fillAmount = (float)GameManager.kingdomSafety / GameManager.maxValue;
        kingdomFaith.fillAmount = (float)GameManager.kingdomFaith / GameManager.maxValue;
        kingdomPeople.fillAmount = (float)GameManager.kingdomPeople / GameManager.maxValue;
        kingdomTreasure.fillAmount = (float)GameManager.kingdomTreasure / GameManager.maxValue;
        kingdomArmy.fillAmount = (float)GameManager.kingdomArmy / GameManager.maxValue;

        //UI impact
        //Right
        if(gameManager.direction == "right")
        {
            if(gameManager.currentCard.kSafetyR != 0)
                kingdomSafetyImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kFaithR != 0)
                kingdomFaithImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kTreasureR != 0)
                kingdomTreasureImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kPeopleR != 0)
                kingdomPeopleImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kArmyR != 0)
                kingdomArmyImpact.transform.localScale = Vector3.one;
        }
        //Left
        else if(gameManager.direction == "left")
        {
            if(gameManager.currentCard.kSafetyL != 0)
                kingdomSafetyImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kFaithL != 0)
                kingdomFaithImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kTreasureL != 0)
                kingdomTreasureImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kPeopleL != 0)
                kingdomPeopleImpact.transform.localScale = Vector3.one;
            if(gameManager.currentCard.kArmyL != 0)
                kingdomArmyImpact.transform.localScale = Vector3.one;
        }
        else
        {
            kingdomSafetyImpact.transform.localScale = Vector3.zero;
            kingdomFaithImpact.transform.localScale = Vector3.zero;
            kingdomTreasureImpact.transform.localScale = Vector3.zero;
            kingdomPeopleImpact.transform.localScale = Vector3.zero;
            kingdomArmyImpact.transform.localScale = Vector3.zero;
        }
    }
    public void AnimationLogic()
    {
        if(gameManager.direction == "right")
        {
        if(gameManager.currentCard.kSafetyR < 0)
            kingdomSafetyAnimator.Play("securityEffectFall");
        if(gameManager.currentCard.kArmyR < 0)
            kingdomArmyAnimator.Play("armyEffectFall");
        if(gameManager.currentCard.kFaithR < 0)
            kingdomFaithAnimator.Play("faithEffectFall");
        if(gameManager.currentCard.kPeopleR < 0)
            kingdomPeopleAnimator.Play("peopleEffectFall");
        if(gameManager.currentCard.kTreasureR < 0)
            kingdomTreasureAnimator.Play("moneyEffectFall");
        
        if(gameManager.currentCard.kSafetyR > 0)
            kingdomSafetyAnimator.Play("securityEffectGrow");
        if(gameManager.currentCard.kArmyR > 0)
            kingdomArmyAnimator.Play("armyEffectGrow");
        if(gameManager.currentCard.kFaithR > 0)
            kingdomFaithAnimator.Play("faithEffectGrow");  
        if(gameManager.currentCard.kPeopleR > 0)
            kingdomPeopleAnimator.Play("peopleEffectGrow");
        if(gameManager.currentCard.kTreasureR > 0)
            kingdomTreasureAnimator.Play("moneyEffectGrow");
        }
        else if(gameManager.direction == "left")
        {
        if(gameManager.currentCard.kSafetyL < 0)
            kingdomSafetyAnimator.Play("securityEffectFall");
        if(gameManager.currentCard.kArmyL < 0)
            kingdomArmyAnimator.Play("armyEffectFall");
        if(gameManager.currentCard.kFaithL < 0)
            kingdomFaithAnimator.Play("faithEffectFall");
        if(gameManager.currentCard.kPeopleL < 0)
            kingdomPeopleAnimator.Play("peopleEffectFall");
        if(gameManager.currentCard.kTreasureL < 0)
            kingdomTreasureAnimator.Play("moneyEffectFall");
        
        if(gameManager.currentCard.kSafetyL > 0)
            kingdomSafetyAnimator.Play("securityEffectGrow");
        if(gameManager.currentCard.kArmyL > 0)
            kingdomArmyAnimator.Play("armyEffectGrow");
        if(gameManager.currentCard.kFaithL > 0)
            kingdomFaithAnimator.Play("faithEffectGrow");  
        if(gameManager.currentCard.kPeopleL > 0)
            kingdomPeopleAnimator.Play("peopleEffectGrow");
        if(gameManager.currentCard.kTreasureL > 0)
            kingdomTreasureAnimator.Play("moneyEffectGrow");
        }
    }
}
