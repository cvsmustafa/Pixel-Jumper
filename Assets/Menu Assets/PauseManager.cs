using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject background; // Arkaplan objesi referans�
    private bool isPaused = false;

    void Start()
    {
        // Oyun ba�lad���nda arkaplan� ve pause men�s�n� gizliyoruz
        pauseMenu.SetActive(false);
        background.SetActive(false);
    }

    void Update()
    {
        Debug.Log("kod �al���yor");
        if (Input.anyKeyDown)
        {
            Debug.Log("Bir tu�a bas�ld�: " + Input.inputString);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape tu�una bas�ld�");
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
        background.SetActive(true); // Arkaplan� g�ster
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        background.SetActive(false); // Arkaplan� gizle
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}