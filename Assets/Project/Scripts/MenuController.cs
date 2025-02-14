using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Analytics;

public class MenuController : MonoBehaviour
{

    public TMP_Text scenarioText;

    [Serializable]
    public struct TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }

    private TransformData locomotion1;
    private TransformData locomotion2;
    private TransformData static1;
    private TransformData static2;

    [SerializeField]
    private GameObject leftHand;
    
    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private GameObject characterModule;
    
    [SerializeField]
    private List<GameObject> positions;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Menu"){
            return;
        }

        // Distinguish between walking and seated scenarios
        string scenarioType = currentScene.name == "SeatedScene" ? "Seated" : "Walking";


        Debug.Log("menucontroller start");
        // on the shelf, behind the books
        locomotion1 = new TransformData
        {
            Position = new Vector3(0.67f, 3.74f, -13.85f),
            Rotation = new Quaternion(0.5f, -0.5f, 0.5f, 0.5f),
            Scale = new Vector3(0.05f, 0.05f, 0.05f)
        };

        // middle of the room (TODO: change this to new hiding place)
        locomotion2 = new TransformData
        {
            Position = new Vector3(0.67f, 3.74f, -0.85f),
            Rotation = Quaternion.Euler(45, 0, 0),
            Scale = new Vector3(0.05f, 0.05f, 0.05f)
        };
        int avatarConfig = PlayerPrefs.GetInt("UseAvatar",0);
        if (avatarConfig == 1){
            Debug.Log("1,Avatar");
            characterModule.SetActive(true);
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            PlayerPrefs.SetInt("UseAvatar",0);


            // Update the TMP text to reflect avatar use and scenario type
            if (scenarioText != null)
            {
                scenarioText.text = $"Scenario: Avatar + {scenarioType}\n\nPlease take off your headset and fill out the SSQ. Come back when you're ready and click the button below:";
            }


        }
        else{
            Debug.Log("2.Hands");
            characterModule.SetActive(false);
            leftHand.SetActive(true);
            rightHand.SetActive(true);
            PlayerPrefs.SetInt("UseAvatar",1);

            if (scenarioText != null)
            {
                scenarioText.text = $"Scenario: No Avatar + {scenarioType}\n\nPlease take off your headset and fill out the SSQ. Come back when you're ready and click the button below:";
            }
        }

        GameObject goldenKey = GameObject.Find("goldenKey");
        Debug.Log($"goldenKey {goldenKey}");
        if (goldenKey != null){
            int keyLocation = PlayerPrefs.GetInt("KeyLocation",0);
            if (keyLocation == 1){
                Debug.Log($"{keyLocation}, position 1");
                goldenKey.transform.position = positions[0].transform.position;
                goldenKey.transform.rotation = positions[0].transform.rotation;
                // goldenKey.transform.localScale = positions[0].transform.localScale;
                // lastUsedLocomotion1 = true;
                PlayerPrefs.SetInt("KeyLocation",0);
                Debug.Log($"1 -> position: {PlayerPrefs.GetInt("KeyLocation",-1)}");   
            }else{
                Debug.Log($"{keyLocation}, position 2");
                goldenKey.transform.position = positions[1].transform.position;
                goldenKey.transform.rotation = positions[1].transform.rotation;
                // goldenKey.transform.localScale = positions[1].transform.localScale;
                PlayerPrefs.SetInt("KeyLocation",1);
                Debug.Log($"0 -> position: {PlayerPrefs.GetInt("KeyLocation",-1)}");   
                // lastUsedLocomotion1 = true;
            }
        }else{
            Debug.LogWarning("goldenKey not found in the scene!");
        }

    }

    public void cleanPlayerRefs() {
        PlayerPrefs.DeleteAll();
    }

    public void StartBttn()
    {
        // SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Project");
    }

    public void SeatedStartBttn()
    {
        // SceneManager.sceneLoaded += OnSceneLoaded;

        //string userFile = PlayerPrefs.GetString("UserFile", null);
        //if (!string.IsNullOrEmpty(userFile))
        //{
        //    Scene currentScene = SceneManager.GetActiveScene();
        //    bool seated = currentScene.name == "SeatedScene";
        //    bool avatar = PlayerPrefs.GetInt("UseAvatar", 0) == 1;

        //    string scenarioIdentifyingData = $"SCENARIO_{(seated ? "seated_" : "walking_")}{(avatar ? "avatar" : "noavatar")}\n";
        //    File.AppendAllText(userFile, scenarioIdentifyingData);
        //}

        SceneManager.LoadScene("SeatedScene");
    }

    public void TestBttn()
    {
        SceneManager.LoadScene("TrainingScenario");
    }

    public void OnSceneLoaded()
    {

        if (UnityEngine.Random.value < 0.5f)
        {
            PlayerPrefs.SetInt("KeyLocation",1);
        }
        else
        {
            PlayerPrefs.SetInt("KeyLocation",0);
        }

        if (UnityEngine.Random.value < 0.5f){
            PlayerPrefs.SetInt("UseAvatar",1);
        }else{
            PlayerPrefs.SetInt("UseAvatar",0);
        }

        // Debug.Log($"scene.name {scene.name}");
        // if (scene.name == "Project")
        // {
        //     // GameObject goldenKey = GameObject.Find("goldenKey");
        //     // Debug.Log($"goldenKey {goldenKey}");
        //     // if (goldenKey != null)
        //     // {
        //     //     TransformData selectedTransform;

        //     //     Debug.Log($"lastUsedLocomotion1 {lastUsedLocomotion1}");
        //     //     // decide which locomotion data to use
        //     //     if (lastUsedLocomotion1 == null)
        //     //     {
        //     //         // first time, pick randomly
        //     //         if (UnityEngine.Random.value < 0.5f)
        //     //         {
        //     //             PlayerPrefs.SetInt("KeyLocation",0);
        //     //         }
        //     //         else
        //     //         {
        //     //             PlayerPrefs.SetInt("KeyLocation",1);
        //     //             selectedTransform = locomotion2;
        //     //             lastUsedLocomotion1 = false;
        //     //         }

        //     //         if (UnityEngine.Random.value < 0.5f){
        //     //             PlayerPrefs.SetInt("UseAvatar",1);
        //     //             // Debug.Log("1.Avatar");
        //     //             // usedAvatar = true;
        //     //             // characterModule.SetActive(true);
        //     //             // leftHand.SetActive(false);
        //     //             // rightHand.SetActive(false);
        //     //         }else{
        //     //             PlayerPrefs.SetInt("UseAvatar",0);
        //     //             // Debug.Log("1.Hands");
        //     //             // characterModule.SetActive(false);
        //     //             // leftHand.SetActive(true);
        //     //             // rightHand.SetActive(true);
        //     //         }
        //     //     }
        //     //     else
        //     //     {
        //     //         // alternate between the two
        //     //         if (lastUsedLocomotion1 == true)
        //     //         {
        //     //             selectedTransform = locomotion2;
        //     //             lastUsedLocomotion1 = false;
        //     //         }
        //     //         else
        //     //         {
        //     //             selectedTransform = locomotion1;
        //     //             lastUsedLocomotion1 = true;
        //     //         }
        //     //         if (PlayerPrefs.GetInt("UseAvatar",0) == 1){
        //     //             Debug.Log("2.Hands");
        //     //             characterModule.SetActive(false);
        //     //             leftHand.SetActive(true);
        //     //             rightHand.SetActive(true);
        //     //         }else{
        //     //             Debug.Log("2.Avatar");
        //     //             characterModule.SetActive(true);
        //     //             leftHand.SetActive(false);
        //     //             rightHand.SetActive(false);
        //     //         }
        //     //     }

        //     //     // apply the selected transform to the goldenKey
        //     //     goldenKey.transform.position = selectedTransform.Position;
        //     //     goldenKey.transform.rotation = selectedTransform.Rotation;
        //     //     goldenKey.transform.localScale = selectedTransform.Scale;
        //     // }
        //     // else
        //     // {
        //     //     Debug.LogWarning("goldenKey not found in the scene!");
        //     // }

        //     SceneManager.sceneLoaded -= OnSceneLoaded;
        // }
    }

    public void QuitGame()
    {
        // TODO: write finishing stuff to CSV file
        Debug.Log("Quitting experiment.");
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetInt("UseAvatar",-1));
        Debug.Log(PlayerPrefs.GetInt("KeyLocation",-1));
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
