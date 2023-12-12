using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float deltaTime;
    void Update()
    {
        // update with average FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // calculate FPS
        float fps = 1.0f / deltaTime;

        // display FPS
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }
}
