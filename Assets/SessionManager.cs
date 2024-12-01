using System;
using System.IO;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private string filePath;
    private DateTime sessionStartTime;
    private DateTime sessionEndTime;

    private void Start()
    {
        string fileName = "camera_data.csv";
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        
        sessionStartTime = DateTime.Now;
        Debug.Log($"Session started at: {sessionStartTime}");

        File.AppendAllText(filePath, "CameraPosition\n");
        InvokeRepeating("SaveCameraData", 0f, 1f);
    }

    //private void SaveSessionData()
    //{
    //    string csvLine = $"Mimi-曹逸辰, {sessionStartTime},{sessionEndTime}\n";
        
    //    // if the file doesnt exist add header for csv
    //    if (!File.Exists(filePath))
    //    {
    //        string header = "Name,Session Start Time,Session End Time\n";
    //        File.WriteAllText(filePath, header);
    //    }
        
    //    // Append the data to the CSV file
    //    File.AppendAllText(filePath, csvLine);
    //    Debug.Log($"Session data saved to: {filePath}");
    //}

    private void SaveCameraData()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        string timestamp = DateTime.Now.ToString();
        string csvPosition = $"{timestamp},{cameraPosition.x},{cameraPosition.y},{cameraPosition.z}\n";
        File.AppendAllText(filePath, csvPosition);
    }

    private void OnApplicationQuit()
    {
        sessionEndTime = DateTime.Now;
        Debug.Log($"Session ended at: {sessionEndTime}");

        //SaveSessionData();
    }
}
