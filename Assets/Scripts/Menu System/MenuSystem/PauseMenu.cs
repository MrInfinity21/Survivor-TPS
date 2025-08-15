using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button quitButton;
    public Button saveButton;
    public Button loadButton;
    public Button mainMenuButton;

    public Transform playerTransform;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSFX;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        resumeButton.onClick.AddListener(() => { PlayClickSound(); ResumeGame(); });
        quitButton.onClick.AddListener(() => { PlayClickSound(); QuitGame(); });
        saveButton.onClick.AddListener(() => { PlayClickSound(); SaveGame(); });
        loadButton.onClick.AddListener(() => { PlayClickSound(); LoadGame(); });
        mainMenuButton.onClick.AddListener(() => { PlayClickSound(); MainMenu(); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    private void PlayClickSound()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);
        } 
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }

    void SaveGame()
    {
        if (playerTransform != null)
        {
            SaveSystem.SavePlayer(playerTransform.position);
        }
    }

    void LoadGame()
    {
        if (playerTransform != null && SaveSystem.HasSavedData())
        {
            Vector3 loadedPosition = SaveSystem.LoadPlayer();

            Debug.Log("Before Load Position: " + playerTransform.position);
            Debug.Log("Loaded Position: " + loadedPosition);

            //Handles CharacterController
            CharacterController cc = playerTransform.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            //Handles NavMeshAgent
            NavMeshAgent agent = playerTransform.GetComponent<NavMeshAgent>();
            if (agent != null) agent.enabled = false;

            //Handles Animator Root Motion
            Animator animator = playerTransform.GetComponent<Animator>();
            if (animator != null) animator.applyRootMotion = false;

            playerTransform.position = loadedPosition;

            if (cc != null) cc.enabled = true;
            if (agent != null) agent.enabled = true;

            Debug.Log("After Load Position: " + playerTransform.position);


            playerTransform.position = loadedPosition;
        }
    }
}
