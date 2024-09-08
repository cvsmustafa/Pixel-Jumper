using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Oyun sahnesini y�kleyin. "Level_1" yerine sahnenizin ad�n� koyun
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        // Oyun derlendi�inde �al���r, edit�rde �al��maz
        Application.Quit();

        // Edit�rde test etmek i�in a�a��daki sat�r� kullanabilirsiniz
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
