using UnityEngine;
using TMPro;


/// <summary>
/// Контроллер главного текста.
/// </summary>
public class TitleTextController : MonoBehaviour
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

    public void SetText(string text)
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
