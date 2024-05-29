using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows = 5;
    public int columns = 5;
    public int baseDurability = 5;
    private float xSpacing = 1.62f;
    private float ySpacing = 1.09f;
    private Vector3 topLeftBrickPosition = new Vector3(-7.86f, 4.28f, 99.736f);
  
    private Color Brick1; // Brick colors
    private Color Brick2;
    private Color Brick3;
    private Color Brick4;
    private Color Brick5;
    private Color[] colors;

    private int BrickHP; // Brick health points
    void Start()
    {
        GenerateBricks();

    }

    void GenerateBricks()
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
                Brick1 = Color.green;    // First color (easiest brick to break)
                Brick2 = Color.blue;     // Second color
                Brick3 = Color.magenta;  // Third color
                Brick4 = Color.red;      // Fourth color
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