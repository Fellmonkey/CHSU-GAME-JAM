using UnityEngine;

public class FPS : MonoBehaviour
{
    private TMPro.TextMeshProUGUI fps;

    void Start()
    {
        fps = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        this.fps.text = fps.ToString();
    }
}
