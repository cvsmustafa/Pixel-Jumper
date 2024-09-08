using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject background; // Arkaplan objesi referansý
    private bool isPaused = false;

    void Start()
    {
        // Oyun baþladýðýnda arkaplaný ve pause menüsünü gizliyoruz
        pauseMenu.SetActive(false);
        background.SetActive(false);
    }

    void Update()
    {
        Debug.Log("kod çalýþýyor");
        if (Input.anyKeyDown)
        {
            Debug.Log("Bir tuþa basýldý: " + Input.inputString);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape tuþuna basýldý");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        background.SetActive(true); // Arkaplaný göster
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        background.SetActive(false); // Arkaplaný gizle
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}