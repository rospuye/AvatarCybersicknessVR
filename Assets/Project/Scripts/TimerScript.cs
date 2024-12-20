using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{

    public GameObject endScreen;
    public float timeRemaining = 120; // Set time in seconds (2 minutes)
    public bool timerIsRunning;
    public TMP_Text TimerText; // Corrected type for TextMeshPro
    
    private Transform userCamera;

    void Start()
    {
        timerIsRunning = true;
        DisplayTime(timeRemaining);
        InvokeRepeating("CallFunction", 1f, 1f);
        userCamera = Camera.main.transform;
    }

    void CallFunction()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= 1;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining); // Ensure timer shows 00:00
                StopFunction();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Ensure no extra second is added
        timeToDisplay = Mathf.Max(0, timeToDisplay);

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StopFunction()
    {
        if (endScreen!=null && userCamera != null){
            endScreen.SetActive(true);
        }
        CancelInvoke("CallFunction");
    }
}
