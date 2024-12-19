using System;
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

    void Start()
    {
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
    }

    public void StartBttn()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Project");
    }

    public void SeatedStartBttn()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("SeatedScene");
    }

    public void TestBttn()
    {
        SceneManager.LoadScene("TrainingScenario");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Project")
        {
            GameObject goldenKey = GameObject.Find("goldenKey");
            if (goldenKey != null)
            {
                TransformData selectedTransform;

                // decide which locomotion data to use
                if (lastUsedLocomotion1 == null)
                {
                    // first time, pick randomly
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        selectedTransform = locomotion1;
                        lastUsedLocomotion1 = true;
                    }
                    else
                    {
                        selectedTransform = locomotion2;
                        lastUsedLocomotion1 = false;
                    }

                    if (UnityEngine.Random.value < 0.5f){
                        usedAvatar = true;
                        characterModule.SetActive(true);
                        leftHand.SetActive(false);
                        rightHand.SetActive(false);
                    }else{
                        characterModule.SetActive(false);
                        leftHand.SetActive(true);
                        rightHand.SetActive(true);
                    }
                }
                else
                {
                    // alternate between the two
                    if (lastUsedLocomotion1 == true)
                    {
                        selectedTransform = locomotion2;
                        lastUsedLocomotion1 = false;
                    }
                    else
                    {
                        selectedTransform = locomotion1;
                        lastUsedLocomotion1 = true;
                    }
                    if (usedAvatar){
                        characterModule.SetActive(false);
                        leftHand.SetActive(true);
                        rightHand.SetActive(true);
                    }else{
                        characterModule.SetActive(true);
                        leftHand.SetActive(false);
                        rightHand.SetActive(false);
                    }
                }

                // apply the selected transform to the goldenKey
                goldenKey.transform.position = selectedTransform.Position;
                goldenKey.transform.rotation = selectedTransform.Rotation;
                goldenKey.transform.localScale = selectedTransform.Scale;
            }
            else
            {
                Debug.LogWarning("goldenKey not found in the scene!");
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void QuitGame()
    {
        // TODO: write finishing stuff to CSV file
        Debug.Log("Quitting experiment.");
        PlayerPrefs.DeleteAll();
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
