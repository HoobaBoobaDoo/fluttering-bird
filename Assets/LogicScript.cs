using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour

{
    public int playerScore;
    public int highScore; // Persisted high score
    public Text scoreText;
    public Text highScoreText; // Optional UI element to show high score
    public GameObject gameOverScreen;
    public GameObject bird;

    public GameObject pauseMenu; // Assign a panel in inspector
    private bool isPaused = false;

    public AudioSource flapSound;
    public AudioSource deathSound;
    public AudioSource scoreSound;

    public void Start()
    {
        playerScore = 0;
        scoreText.text = playerScore.ToString();
        gameOverScreen.SetActive(false);
        bird = GameObject.FindGameObjectWithTag("Bird");

        // Load persisted high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }


    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        if (bird.GetComponent<BirdScript>().birdAlive)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();
            scoreSound.Play();

            // Update and persist high score if beaten
            if (playerScore > highScore)
            {
                highScore = playerScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
                if (highScoreText != null)
                {
                    highScoreText.text = highScore.ToString();
                }
            }
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void gameOver()
    {
        Debug.Log("Game Over!");
        gameOverScreen.SetActive(true);
        // Ensure high score UI updates even if last score event didn't trigger
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScene");

    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }

     // Pause controls
    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        if (gameOverScreen.activeSelf) return; // Do not pause over game over
        Time.timeScale = 0f;
        isPaused = true;
        if (pauseMenu != null) pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pauseMenu != null) pauseMenu.SetActive(false);
    }

}
