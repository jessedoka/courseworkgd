using TMPro;
using UnityEngine;

public class Memory : MonoBehaviour
{
    // using TMPro;
    public TextMeshProUGUI memoryText;

    public float deltaTime;
    void Update()
    {
        
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // using System memory calculate memory in MB
        float memory = System.GC.GetTotalMemory(false) / 100000000f;

        memoryText.text = Mathf.Round(memory).ToString() + " MB";
    }
}
