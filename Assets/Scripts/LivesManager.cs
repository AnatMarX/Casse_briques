using System.Text;
using TMPro;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int lives = 5;
    
    void Start()
    {
        UpdateLivesText();
    }

    public void LoseOneLife()
    {
        lives --;
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        scoreText.text = Multiply("_ ",lives);
    }
    public static string Multiply(string source, int multiplier) // To get a string multiple times in a row
    {
        StringBuilder sb = new StringBuilder(multiplier * source.Length);
        for (int i = 0; i < multiplier; i++)
        {
            sb.Append(source);
        }

        return sb.ToString();
    }
}