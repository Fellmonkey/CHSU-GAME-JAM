using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Card card;
    [SerializeField] private BoxCollider2D thisCard;
    [SerializeField] private bool _isMouseOver;

    public bool IsMouseOver {get{return _isMouseOver;}}

    private void OnMouseOver() {
        _isMouseOver = true;
    }
    private void OnMouseExit() {
        _isMouseOver = false;
    }

}
public enum CardSprite
{
    MAN,
    OLD_MAN,
    NARIC
}
