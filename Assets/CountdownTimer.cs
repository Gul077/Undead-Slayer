using System;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public GameObject countdownDisplay; // Reference to the panel containing the countdown text

    public TextMeshProUGUI countdownText;
    public GameObject goTextObject;

    public float countdownDuration = 3f; // Duration of countdown in seconds

    public static event Action OnCountdownComplete;

    private void Start()
    {
        StartCountdown();
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    private System.Collections.IEnumerator CountdownRoutine()
    {
        int count = (int)countdownDuration;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        // Countdown complete, show "GO!" text
        countdownDisplay.SetActive(false); // Hide the panel containing the countdown text
        goTextObject.SetActive(true);

        // Fire event to signal countdown complete
        OnCountdownComplete?.Invoke();
    }
}
