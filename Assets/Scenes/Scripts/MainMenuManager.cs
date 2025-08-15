using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button tutorialButton;
    public Button quitButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSFX;


    public void Start()
    {
        if (startButton != null) startButton.onClick.AddListener(() => { PlayClickSound(); PlayLevel(); });
        if (tutorialButton != null) tutorialButton.onClick.AddListener(() => { PlayClickSound(); PlayTutorial(); });
        if (quitButton != null) quitButton.onClick.AddListener(() => { PlayClickSound(); QuitGame(); });
        
    }

    private void PlayClickSound()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);
        }
    }
    public void PlayLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

}
