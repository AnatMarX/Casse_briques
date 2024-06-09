using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class HighscoresManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTextEasy; // Référence au TextMesh Pro UI dans le Scroll View
    public TextMeshProUGUI scoreTextMedium; // Référence au TextMesh Pro UI dans le Scroll View
    public TextMeshProUGUI scoreTextHard; // Référence au TextMesh Pro UI dans le Scroll View
    string[] difficulties = { "Easy", "Medium", "Hard" };
    
    void Start()
    {
        TextMeshProUGUI[] texts = { scoreTextEasy, scoreTextMedium, scoreTextHard };
        for (int i = 0; i <= 2; i++)
        {

            DisplayScores(difficulties[i], texts[i]);
        }
    }

    void DisplayScores(string difficulty, TextMeshProUGUI scoreText)
    {
        
        string filePath = Application.dataPath + "/Scores"+difficulty+".csv";

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<ScoreEntry> scoreEntries = new List<ScoreEntry>();

            for (int i = 1; i < lines.Length; i++) // Commencez à 1 pour ignorer l'en-tête
            {
                string[] fields = lines[i].Split(',');

                if (fields.Length >= 2)
                {
                    string name = fields[0];
                    if (int.TryParse(fields[1], out int score))
                    {
                        scoreEntries.Add(new ScoreEntry { Name = name, Score = score });
                    }
                }
            }

            // Tri des scores par ordre décroissant
            scoreEntries = scoreEntries.OrderByDescending(entry => entry.Score).ToList();

            string displayText = "";
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                displayText += $"{i + 1}. \"{scoreEntries[i].Name}\" - {scoreEntries[i].Score}\n";
            }

            scoreText.text = displayText;
        }
        else
        {
            scoreText.text = "No scores available.";
        }
    }

    // Classe pour stocker les entrées de score
    private class ScoreEntry
    {
        public string Name;
        public int Score;
    }
}
