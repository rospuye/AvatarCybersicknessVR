using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public void ReturnBttn()
    {
        SceneManager.LoadScene("Menu");

    }
}
