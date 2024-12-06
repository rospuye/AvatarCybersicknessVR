using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining = 120; // Set time in seconds (2 minutes)
    public bool timerIsRunning;
    public TMP_Text TimerText; // Corrected type for TextMeshPro
    
    void Start()
    {
        timerIsRunning = true;
        DisplayTime(timeRemaining);
        InvokeRepeating("CallFunction", 1f, 1f);
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
        CancelInvoke("CallFunction");
    }
}
