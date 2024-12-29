using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InputFieldHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public static string fileName;
    [SerializeField]
    private Button enterButton;

    public void CheckInputValue(){
        string inputText = inputField.text;

        if (!string.IsNullOrWhiteSpace(inputText)){
            enterButton.interactable = true;
        }
        else
        {
            enterButton.interactable = false;
            // Debug.LogError("Input text is empty! Cannot set filename.");
        }
        
    }
    public void OnEnterButtonClicked()
    {
        string inputText = inputField.text;
        enterButton.interactable = true;
        fileName = inputText + ".csv";
        Debug.Log("Filename set: " + fileName);
    }

    public void OnIDConfirmation()
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Debug.LogError("Filename is not set! Cannot create a file.");
            return;
        }

        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string csvContent = "";
        File.WriteAllText(filePath, csvContent);

        Debug.Log("CSV file created: " + filePath);
        PlayerPrefs.SetString("UserFile", fileName);
    }
}
