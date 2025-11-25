using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Pause everything except UI
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        // Resume game
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
