using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Backbutton : MonoBehaviour
{

    public Button backButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSFX;

    public void Start()
    {
        if (backButton != null) backButton.onClick.AddListener(() => { PlayClickSound(); BackMenu(); });

    }

    private void PlayClickSound()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);
        }
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
