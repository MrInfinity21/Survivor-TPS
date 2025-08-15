using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverTrigger : MonoBehaviour
{
    public float delay = 2f;

    private void Start()
    {
        Invoke("LoadMainMenu", delay);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
