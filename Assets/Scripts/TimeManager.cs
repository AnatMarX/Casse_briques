using System.Text;
using TMPro;
using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    private LevelController LevelController;
    public TextMeshProUGUI scoreText;
    private int time = 300;
    private bool isCountingDown = false;
    private bool isPaused = false;
    private Coroutine countdownCoroutine;

    void Start()
    {
        UpdateTimeText();
    }

    public void BonusTime()
    {
        time += 30;
        UpdateTimeText();
    }

    public void StartTimeCountdown()
    {
        if (!isCountingDown)
        {
            countdownCoroutine = StartCoroutine(TimeCountdownCoroutine());
            Debug.Log("Coroutine started");
        }
        else if (isPaused)
        {
            isPaused = false;
            Debug.Log("Countdown resumed");
        }
    }

    public void StopTimeCountdown()
    {
        isPaused = true;
        Debug.Log("Countdown paused");
    }

    private IEnumerator TimeCountdownCoroutine()
    {
        Debug.Log("Coroutine started for sure");
        isCountingDown = true;
        while (time > 0)
        {
            while (isPaused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1);
            time--;
            UpdateTimeText();
            Debug.Log("Time left: " + time);
        }
        isCountingDown = false;
        LevelController.Death();
    }

    public void Update()
    {

    }

    private void UpdateTimeText()
    {
        scoreText.text = time + "s";
    }
}