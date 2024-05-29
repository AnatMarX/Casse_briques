using System.Text;
using TMPro;
using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public LevelController LevelController = FindObjectOfType<LevelController>();
    public TextMeshProUGUI scoreText;
    private int time = 300;
    private bool isCountingDown = false;

    void Start()
    {
        UpdateTimeText();
    }

    public void BonusTime()
    {
        time+=30;
        UpdateTimeText();
    }

    public void StartTimeCountdown()
    {
        StartCoroutine(TimeCountdownCoroutine());
    }

    private IEnumerator TimeCountdownCoroutine()
    {
        isCountingDown = true;
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            Debug.Log("Time left: " + time);
        }
        isCountingDown = false;
        LevelController.Death();
    }

    private void UpdateTimeText()
    {
        scoreText.text = time+"s";
    }
}