using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TMP_Text playerNameInput;
    public TextMeshProUGUI scoreText;
    ScoreManager scoreManager;
    string playerScore;
    public BallController ballController;

    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
        // Assurez-vous que l'écran de fin est désactivé au début
        gameOverScreen.SetActive(false);

        // Ajoutez un listener au bouton pour appeler la méthode SubmitScore lorsque le bouton est cliqué
        // submitButton.onClick.AddListener(SubmitScore);
    }

    // Méthode pour appeler la fin du jeu
    public void GameOver()
    {
        ballController.DestroyBall();
        this.playerScore = scoreManager.GetScore().ToString();
        // Affiche l'écran de fin
        gameOverScreen.SetActive(true);

        // Affiche le score du joueur (remplacez 'playerScore' par le score réel)
        scoreText.text = "Final Score: " + this.playerScore;
    }

    // Méthode pour soumettre le score
    public void SubmitScore()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty","Easy");
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            // Enregistrez le score dans un fichier CSV
            string filePath = Application.dataPath + "/Scores"+difficulty+".csv";
            string delimiter = ",";

            if (!File.Exists(filePath))
            {
                // Créez le fichier et ajoutez l'en-tête si le fichier n'existe pas encore
                File.WriteAllText(filePath, "Name" + delimiter + "Score\n");
            }

            // Ajoutez le nouveau score
            string newLine = playerName + delimiter + this.playerScore + "\n";
            File.AppendAllText(filePath, newLine);

            // Vous pouvez ajouter une logique ici pour réinitialiser le jeu ou rediriger vers un autre écran
            Debug.Log("Score submitted: " + playerName + " - " + this.playerScore);
            SceneManager.LoadScene("main_menu");
        }
    }
}
