using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject InputPanel;
    
    [SerializeField]
    private GameObject Menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // if Userfile has not been recorded, 
        // open input panel to record user's id and vice versa
        Debug.Log( currentScene.name + " and " + PlayerPrefs.GetString("UserFile","none"));
        if (currentScene.name == "Menu" && PlayerPrefs.GetString("UserFile","none") == "none"){
            InputPanel.SetActive(true);
            Menu.SetActive(false);
        }else{
            InputPanel.SetActive(false);
            Menu.SetActive(true);
        }

    }

}
