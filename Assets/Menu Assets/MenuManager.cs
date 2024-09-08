using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Oyun sahnesini yükleyin. "Level_1" yerine sahnenizin adýný koyun
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        // Oyun derlendiðinde çalýþýr, editörde çalýþmaz
        Application.Quit();

        // Editörde test etmek için aþaðýdaki satýrý kullanabilirsiniz
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
