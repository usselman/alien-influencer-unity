using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public TMP_Text scoreText;
    private int score = 0;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

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
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        score = 0;
        UpdateScore(0);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");

        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        score = 0;
        UpdateScore(0);
        Time.timeScale = 1f;

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
