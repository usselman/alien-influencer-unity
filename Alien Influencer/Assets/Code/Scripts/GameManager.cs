using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes
using TMPro; // Required for TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern to easily access the instance

    public GameObject startMenu; // Reference to the Start Menu UI panel
    public GameObject gameOverMenu; // Reference to the Game Over Menu UI panel
    public GameObject winMenu; // Reference to the Win Menu UI panel
    public TMP_Text scoreText; // Reference to the Score Text UI element
    private int score = 0; // The current score

    private void Awake()
    {
        // Ensure there's only one instance of this object in the game
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        score = 0;
        UpdateScore(0);
        Time.timeScale = 1f; // Resume the game
        // Add any other initialization code for starting the game here
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void WinGame()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");

        // Hide all menus and reset game state
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        score = 0;
        UpdateScore(0);
        Time.timeScale = 1f; // Ensure the game is running

        // Reload the scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore(score);
        if (score >= 100)
        {
            WinGame();
        }
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    // Wrapper methods for UI buttons
    public void OnStartGameButton()
    {
        Debug.Log("Starting Game...");
        StartGame();
    }

    public void OnRestartGameButton()
    {
        Debug.Log("Restarting Game via Button...");
        RestartGame();
    }
}
