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
        // Assurez-vous que l'�cran de fin est d�sactiv� au d�but
        gameOverScreen.SetActive(false);

        // Ajoutez un listener au bouton pour appeler la m�thode SubmitScore lorsque le bouton est cliqu�
        // submitButton.onClick.AddListener(SubmitScore);
    }

    // M�thode pour appeler la fin du jeu
    public void GameOver()
    {
        ballController.DestroyBall();
        this.playerScore = scoreManager.GetScore();
        // Affiche l'�cran de fin
        gameOverScreen.SetActive(true);

        // Affiche le score du joueur (remplacez 'playerScore' par le score r�el)
        scoreText.text = "Final Score: " + this.playerScore;
    }

    public void Victory()
    {
        // Affiche "Victoire"
        timeManager.StopTimeCountdown();
        announcementText.text = "Victoire!";
        ballController.DestroyBall();
        this.playerScore = scoreManager.GetScore();
        // Affiche l'�cran de fin
        gameOverScreen.SetActive(true);
        // Ajouter un bonus de temps au score du joueur (exponentiel, inversement li� au temps pris par le joueur pour finir le niveau) (exp 9.21 ~ 10 000 = bonus max)
        float elapsedTime = timeManager.getElapsedTime();
        float initialTime = timeManager.getInitialTime();
        int timeBonus = Mathf.FloorToInt(Mathf.Exp(9.21f * (1 - elapsedTime / initialTime)));
        this.playerScore += timeBonus; // Mise � jour du score total
        // Affiche le score du joueur (remplacez 'playerScore' par le score r�el)
        scoreText.text = "Final Score: " + this.playerScore + " (" +timeBonus+" bonus de temps)";
    }

    // Sauvegarder le score du joueur dans le fichier correspondant � la difficult� du niveau
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
                // Cr�ez le fichier et ajoutez l'en-t�te si le fichier n'existe pas encore
                File.WriteAllText(filePath, "Name" + delimiter + "Score\n");
            }

            // Ajoutez le nouveau score
            string newLine = playerName + delimiter + this.playerScore + "\n";
            File.AppendAllText(filePath, newLine);
            Debug.Log("Score submitted: " + playerName + " - " + this.playerScore);
        }
    }

    // M�thode pour soumettre le score et retourner au menu principal
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