using UnityEngine;
using UnityEngine.UI; // Required for manipulating UI elements
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton pattern to easily access the instance
    public TMP_Text scoreText; // The UI Text component that displays the score
    private int score = 0; // The current score

    private void Awake()
    {
        // Ensure there's only one instance of this object in the game
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points; // Increase the score
        scoreText.text = "Score: " + score; // Update the UI

        if (score >= 100)
        {
            GameManager.Instance.WinGame();
        }
    }
}
