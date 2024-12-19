using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartBttn()
    {
        SceneManager.LoadScene("Project");

    }

    public void TestBttn()
    {
        SceneManager.LoadScene("TrainingScenario");

    }
}
