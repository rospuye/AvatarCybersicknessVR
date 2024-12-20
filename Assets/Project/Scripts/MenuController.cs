using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
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

    private static bool? lastUsedLocomotion1 = null;

    [SerializeField]
    private GameObject leftHand;
    
    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private GameObject characterModule;
    private bool usedAvatar = false;
    
    [SerializeField]
    private List<GameObject> positions;
    void Start()
    {
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

        if (PlayerPrefs.GetInt("UseAvatar",0) == 1){
            Debug.Log("1,Avatar");
            characterModule.SetActive(true);
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            PlayerPrefs.SetInt("UseAvatar",0);
        }else{
            Debug.Log("2.Hands");
            characterModule.SetActive(false);
            leftHand.SetActive(true);
            rightHand.SetActive(true);
            PlayerPrefs.SetInt("UseAvatar",1);
        }

        GameObject goldenKey = GameObject.Find("goldenKey");
        Debug.Log($"goldenKey {goldenKey}");
        if (goldenKey != null){
            if (PlayerPrefs.GetInt("KeyLocation",0) == 0){
                goldenKey.transform.position = positions[0].transform.position;
                goldenKey.transform.rotation = positions[0].transform.rotation;
                // goldenKey.transform.localScale = positions[0].transform.localScale;
                // lastUsedLocomotion1 = true;
                PlayerPrefs.SetInt("KeyLocation",1);
            }else{
                goldenKey.transform.position = positions[1].transform.position;
                goldenKey.transform.rotation = positions[1].transform.rotation;
                // goldenKey.transform.localScale = positions[1].transform.localScale;
                PlayerPrefs.SetInt("KeyLocation",0);
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
            PlayerPrefs.SetInt("KeyLocation",0);
        }
        else
        {
            PlayerPrefs.SetInt("KeyLocation",1);
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
