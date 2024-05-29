using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickController : MonoBehaviour
{
    private Renderer rend;
    private Color[] colors;
    private int currentDurability;
    private int maxDurability;
    private ScoreManager scoreManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager in the scene
    }

    public void Initialize(int Durability, Color[] colorsArray)
    {
        maxDurability = Durability;
        currentDurability = Durability;
        colors = colorsArray;

        // Ensure renderer is initialized
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }

        SetColor();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentDurability--;

        if (currentDurability > 0)
        {
            SetColor();
            scoreManager.AddScore(10); // Add score when the brick is destroyed
        }
        else
        {
            scoreManager.AddScore(maxDurability*100); // Add score when the brick is destroyed
            Destroy(gameObject);
        }
    }

    void SetColor()
    {
        if (currentDurability > 0 && currentDurability <= colors.Length)
        {
            rend.material.color = colors[currentDurability-1];
        }
    }
}
