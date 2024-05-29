using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LivesManager LivesManager;
    public GameObject Lives;
    public GameObject PlayerAndBall;
    public GameObject Ball;
    public GameObject brickPrefab;
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    private string difficulty;
    private int rows = 10;
    private int columns = 7;
    private int baseDurability = 5;
    private float xSpacing = 1.62f;
    private float ySpacing = 1.09f;
    private Vector3 topLeftBrickPosition = new Vector3(-7.86f, 4.28f, 99.736f);

    public Vector3 initialPositionPlayerAndBall = new Vector3(0f, -4.493144f, 99.51f);
    public Vector3 initialPositionBall = new Vector3(2.654874f, 0f, -0.03000641f);

    private Color Brick1; // Brick colors
    private Color Brick2;
    private Color Brick3;
    private Color Brick4;
    private Color Brick5;
    private Color[] colors;
    private bool alive = true;
    
    private int BrickHP; // Brick health points
    void Start()
    {
        difficulty = PlayerPrefs.GetString("Difficulty");
        GenerateBricks(difficulty);

    }

    public void Death()
    {
        alive = LivesManager.LoseOneLife();
        if (alive)
        {
            resetPlayer();
        }
    }

    public void resetPlayer()
    {
        PlayerAndBall.transform.position = initialPositionPlayerAndBall;

        // Fixer Ball à l'emplacement spécifié
        Ball.transform.position = initialPositionBall;

        // Rendre Ball enfant de PlayerAndBall
        Ball.transform.SetParent(PlayerAndBall.transform);

        Debug.Log("Les objets ont été positionnés et Ball est maintenant enfant de PlayerAndBall.");
    }

    public void StartLevel()
    {

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
                Debug.Log(colors[0]);
                Debug.Log(colors[1]);
                Debug.Log(colors[2]);
                // Initialize the brick with the defined durability and colors
                BrickController brickController = brick.GetComponent<BrickController>();
                brickController.Initialize(durability, colors);
            }
        }
    }
}