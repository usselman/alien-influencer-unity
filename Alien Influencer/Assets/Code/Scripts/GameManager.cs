using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public TMP_Text scoreText;
    public TMP_Text timeLeftText;
    private float timeLeft = 121f;
    private int score = 0;
    int minutes = 0;
    int seconds = 0;

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
    private void Update()
    {
        timeLeft -= Time.deltaTime;

        minutes = Mathf.FloorToInt(timeLeft / 60);
        seconds = Mathf.FloorToInt(timeLeft % 60);
        timeLeftText.text = string.Format("Time Left: {0}:{1:00}", minutes > 0? minutes : 0, seconds > 0 ? seconds : 0);

        if (timeLeft <= 0)
        {
            WinGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore(score);
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
