using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    TimeManager timeManager;
    ScoreManager scoreManager;
    public GameObject gameOverScreen;
    public TMP_Text playerNameInput;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI announcementText;
    int playerScore;
    public BallController ballController;
    
    void Start()
    {
        timeManager = GetComponent<TimeManager>();
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
        this.playerScore = scoreManager.GetScore();
        // Affiche l'écran de fin
        gameOverScreen.SetActive(true);

        // Affiche le score du joueur (remplacez 'playerScore' par le score réel)
        scoreText.text = "Final Score: " + this.playerScore;
    }

    public void Victory()
    {
        // Affiche "Victoire"
        timeManager.StopTimeCountdown();
        announcementText.text = "Victoire!";
        ballController.DestroyBall();
        this.playerScore = scoreManager.GetScore();
        // Affiche l'écran de fin
        gameOverScreen.SetActive(true);
        // Ajouter un bonus de temps au score du joueur (exponentiel, inversement lié au temps pris par le joueur pour finir le niveau) (exp 9.21 ~ 10 000 = bonus max)
        float elapsedTime = timeManager.getElapsedTime();
        float initialTime = timeManager.getInitialTime();
        int timeBonus = Mathf.FloorToInt(Mathf.Exp(9.21f * (1 - elapsedTime / initialTime)));
        this.playerScore += timeBonus; // Mise à jour du score total
        // Affiche le score du joueur (remplacez 'playerScore' par le score réel)
        scoreText.text = "Final Score: " + this.playerScore + " (" +timeBonus+" bonus de temps)";
    }

    // Sauvegarder le score du joueur dans le fichier correspondant à la difficulté du niveau
    private void SaveScore()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            // Enregistrez le score dans un fichier CSV
            string filePath = Application.dataPath + "/Scores" + difficulty + ".csv";
            string delimiter = ",";

            if (!File.Exists(filePath))
            {
                // Créez le fichier et ajoutez l'en-tête si le fichier n'existe pas encore
                File.WriteAllText(filePath, "Name" + delimiter + "Score\n");
            }

            // Ajoutez le nouveau score
            string newLine = playerName + delimiter + this.playerScore + "\n";
            File.AppendAllText(filePath, newLine);
            Debug.Log("Score submitted: " + playerName + " - " + this.playerScore);
        }
    }

    // Méthode pour soumettre le score et retourner au menu principal
    public void MainMenuButton()
    {
        SaveScore();
        // Retour au menu
        SceneManager.LoadScene("main_menu");
        
    }

    

    // Save the score and restart the level
    public void RestartButton()
    {
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}