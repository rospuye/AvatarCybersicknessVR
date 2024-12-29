using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    private string filePath;
    private Vector3 previousPosition;
    private bool isRecording = true;
    public TimerScript timerScript;

    private void Start()
    {
        //string timestamp = DateTime.Now.ToString("MM-dd-yy_H-mm-ss");
        //string fileName = $"session_data{timestamp}.csv";
        string fileName = PlayerPrefs.GetString("UserFile", null);
        previousPosition = Camera.main.transform.position;

        if (!string.IsNullOrEmpty(fileName))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            bool seated = currentScene.name == "SeatedScene";
            bool avatar = PlayerPrefs.GetInt("UseAvatar", 0) == 1;

            string scenarioIdentifyingData = $"SCENARIO_{(seated ? "seated_" : "walking_")}{(avatar ? "avatar" : "noavatar")}\n";
            File.AppendAllText(fileName, scenarioIdentifyingData);

            filePath = Path.Combine(Application.persistentDataPath, fileName);
            //File.AppendAllText(filePath, "MOVEMENT_DATA\n");
            File.AppendAllText(filePath, "Timestamp,MovementDeltaX,MovementDeltaY,MovementDeltaZ,TotalMovementDelta\n");
            InvokeRepeating("SaveMovementData", 0f, 1f);
        }
    }

    private void SaveMovementData()
    {
        if (!isRecording) return;

        Vector3 currentPosition = Camera.main.transform.position;
        Vector3 movementDelta = currentPosition - previousPosition;
        previousPosition = currentPosition;
        float totalMovementDelta = movementDelta.magnitude;

        string timestamp = DateTime.Now.ToString();
        string csvData = $"{timestamp},{movementDelta.x},{movementDelta.y},{movementDelta.z},{totalMovementDelta}\n";
        File.AppendAllText(filePath, csvData);
    }

    public void StopRecordingMovement()
    {
        isRecording = false;
        Debug.Log("Movement recording stopped.");

        if (timerScript != null)
        {
            //File.AppendAllText(filePath, "TIMER_DATA\n");
            float timeLeft = timerScript.timeRemaining;
            string timestamp = DateTime.Now.ToString();
            string remainingTimeData = $"TimerStopped,{timestamp},TimeLeft:{timeLeft}s\n";
            File.AppendAllText(filePath, remainingTimeData);
        }
        else
        {
            Debug.LogWarning("TimerScript reference not set in SessionManager.");
        }
    }

}