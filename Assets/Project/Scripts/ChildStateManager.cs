using UnityEngine;
using UnityEngine.SceneManagement;

public class ChildStateManager : MonoBehaviour
{
    public GameObject child1; // The first child (can be active on scene start)
    public GameObject child2; // The second child (should be inactive initially)

    private void Start()
    {
        // If child1 is active on start, store that state
        int savedValue = PlayerPrefs.GetInt("ActiveChild", 0);
        Debug.Log("savedValue: "+savedValue);
        if (savedValue == 0 || savedValue == 2 )
        {
            PlayerPrefs.SetInt("ActiveChild", 1); // Store that child1 was active
            child1.SetActive(true);
            child2.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("ActiveChild", 2); // Otherwise, child2 is active
            child1.SetActive(false);
            child2.SetActive(true);
        }
    }

    // Call this method when child1 is clicked to reload the scene
    public void OnChild1Clicked()
    {
        // Store that child1 is clicked and should become inactive
        PlayerPrefs.SetInt("ActiveChild", 1);

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
