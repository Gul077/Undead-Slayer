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
    private bool gameIsActive = true;

    public int targetScore = 100;

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
        youWinPanel.SetActive(false);
        youLosePanel.SetActive(false);
    }

    void Update()
    {
        if (gameIsActive)
        {
            CountDownTimer();
            CheckWinCondition();
        }
    }

    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreText();
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
            UpdateTimerText();
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
}
