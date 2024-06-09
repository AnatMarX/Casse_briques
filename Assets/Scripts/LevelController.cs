using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    ScoreManager scoreManager;
    AudioManager audioManager;
    GameOverManager gameOverManager;
    TimeManager timeManager;
    LivesManager livesManager;
    BallController ballController;
    public GameObject Player;
    public GameObject Ball;
    public GameObject brickPrefab;
    public GameObject boxPrefab;
    private string difficulty;
    private readonly int rows = 10;
    private readonly int columns = 7;
    private readonly int baseDurability = 5;
    private float xSpacing = 1.62f;
    private float ySpacing = 1.09f;
    private Vector3 topLeftBrickPosition = new Vector3(-7.86f, 4.28f, 99.736f);

    private Vector3 initialPositionPlayerAndBall = new Vector3(0f, -4.493144f, 99.51f);
    private Vector3 initialPositionBall = new Vector3(0f, -3.483143f, 99.51f);

    private Color Brick1; // Brick colors
    private Color Brick2;
    private Color Brick3;
    private Color Brick4;
    private Color Brick5;
    private Color[] colors;
    private bool alive = true;
    
    private int BrickHP; // Brick health points

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
        gameOverManager = GetComponent<GameOverManager>();
        livesManager = GetComponent<LivesManager>();
        timeManager = GetComponent<TimeManager>();

        difficulty = PlayerPrefs.GetString("Difficulty");
        GenerateBricks(difficulty);
    }

    public void KillPlayer()
        // Player loses a life
    {
        StopLevel();
        alive = livesManager.LoseOneLife();
        if (alive)
        {
            ResetPlayer();
        }
        else
        {
            Death();
        }
    }

    public void Death()
        // True end of the level
    {
        audioManager.PlaySFX(audioManager.death);
        gameOverManager.GameOver();
    }

    public void ResetPlayer()
    {
        ballController = Ball.GetComponent<BallController>();
        ballController.SetBallSpeed(0);
        ballController.SetImpulse();
        Player.transform.position = initialPositionPlayerAndBall;

        // Fixer Ball à l'emplacement spécifié
        Ball.transform.position = initialPositionBall;

        // Rendre Ball enfant de PlayerAndBall
        Ball.transform.SetParent(Player.transform);

        Debug.Log("Les objets ont été positionnés et Ball est maintenant enfant de PlayerAndBall.");
    }

    public void StartLevel()
    {
        Debug.Log("start countdown");
        timeManager.StartTimeCountdown();
    }

    public void StopLevel() {
        Debug.Log("Stop countdown");
        timeManager.StopTimeCountdown();
    }

    void GenerateBricks(string difficulty)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(i * xSpacing, - j * ySpacing, 0);
                position += topLeftBrickPosition;
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);

                // Randomize durability if needed, or use baseDurability
                int durability = baseDurability; // Or you can randomize this value

                // Init colors
                Brick1 = Color.blue;    // First color (easiest brick to break)
                Brick2 = Color.Lerp(Color.blue, Color.magenta, 0.5f);     // Second color
                Brick3 = Color.magenta;  // Third color
                Brick4 = Color.Lerp(Color.black,Color.magenta,0.5f);      // Fourth color
                Brick5 = Color.black;    // Fifth color (hardest brick to break)

                // Define the color array based on durability
                Color[] colors = new Color[] { Brick1, Brick2, Brick3, Brick4, Brick5 };
                /*
                Debug.Log(colors[0]);
                Debug.Log(colors[1]);
                Debug.Log(colors[2]);
                */

                // Initialize the brick with the defined durability and colors
                BrickController brickController = brick.GetComponent<BrickController>();
                brickController.Initialize(durability, colors);
            }
        }
    }
}