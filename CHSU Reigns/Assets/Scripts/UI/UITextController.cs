using UnityEngine;
using TMPro;


/// <summary>
///  онтроллер главного текста.
/// </summary>
public class UITextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private float speedAnimation;
    [SerializeField] private string newText;
    [SerializeField] private bool flagAnimation;

    private void Update()
    {
        Color newColor = titleText.color;

        if (flagAnimation)
        {
            newColor.a = Mathf.MoveTowards(newColor.a, 0, speedAnimation * Time.deltaTime);

            if (newColor.a == 0)
            {
                titleText.text = newText;
                flagAnimation = false;
            }
        }
        else
        {
            newColor.a = Mathf.MoveTowards(newColor.a, 1, speedAnimation * Time.deltaTime);

            if (newColor.a == 1)
            {
                enabled = false;
            }
        }

        titleText.color = newColor;
    }

    /// <summary>
    /// ѕоказывает текст, который передаетс€ в качестве параметра. 
    /// ≈сли нужно скрыть текст, то в качестве параметра нужно отправить пустую строку.
    /// </summary>
    /// <param name="text"></param>
    public void ShowText(string text) 
    {
        if (enabled && !flagAnimation)
        {
            Color color = titleText.color;
            color.a = 1;
            titleText.color = color;

            newText = text;
        }

        newText = text;
        enabled = true;
        flagAnimation = true;
    }
}
