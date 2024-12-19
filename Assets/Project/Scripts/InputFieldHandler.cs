using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public static string fileName;

    public void OnEnterButtonClicked()
    {
        string inputText = inputField.text;

        if (!string.IsNullOrWhiteSpace(inputText))
        {
            fileName = inputText + ".csv";
            Debug.Log("Filename set: " + fileName);
        }
        else
        {
            Debug.LogError("Input text is empty! Cannot set filename.");
        }
    }

    public void OnIDConfirmation()
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Debug.LogError("Filename is not set! Cannot create a file.");
            return;
        }

        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string csvContent = "Header1,Header2,Header3\nValue1,Value2,Value3";
        File.WriteAllText(filePath, csvContent);

        Debug.Log("CSV file created: " + filePath);
    }
}
