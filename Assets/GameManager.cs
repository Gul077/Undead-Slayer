using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject youWinPanel;
    public GameObject youLosePanel;

    private int score = 0;
    private float timeRemaining = 120f; // 2 minutes
    private bool gameIsActive = false;
    private bool canUpdateTimerAndScore = true; // Flag to control updating of timer and score

    public int targetScore = 100;

    void Start()
    {
        CountdownTimer.OnCountdownComplete += StartGame;
        youWinPanel.SetActive(false);
        youLosePanel.SetActive(false);
    }

    void OnDestroy()
    {
        CountdownTimer.OnCountdownComplete -= StartGame;
    }

    void Update()
    {
        if (gameIsActive && canUpdateTimerAndScore) // Only update timer and score if the game is active and the flag is true
        {
            CountDownTimer();
            CheckWinCondition();
            UpdateTimerText(); // Update timer only when the game is active
        }
    }

    public void AddScore(int pointsToAdd)
    {
        if (canUpdateTimerAndScore) // Only update score if the flag is true
        {
            score += pointsToAdd;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}/{targetScore}";
    }

    void CountDownTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            gameIsActive = false;
            CheckLoseCondition();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    void CheckWinCondition()
    {
        if (score >= targetScore)
        {
            youWinPanel.SetActive(true);
            gameIsActive = false;
        }
    }

    void CheckLoseCondition()
    {
        if (score < targetScore)
        {
            youLosePanel.SetActive(true);
        }
    }

    void StartGame()
    {
        gameIsActive = true;
        canUpdateTimerAndScore = true; // Set the flag to true when the game starts
        Debug.Log("Game started!");
    }
}
