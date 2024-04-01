using TMPro;
using UnityEngine;

public class ConsoleText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;
    
    public void SetText(string text)
    {
        tmpText.text = text;
    }
}
