using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInputText : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI displayText;

    public void UpdateText()
    {
        displayText.text = inputField.text;
    }
}
