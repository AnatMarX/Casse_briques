using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour
{
    public GameObject BonusPrefab;
    private Renderer rend;
    private Color[] colors;
    private int currentDurability;
    private int maxDurability;
    private ScoreManager scoreManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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

        SetBoxColor();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") | collision.gameObject.CompareTag("BonusBall"))
        {
            this.TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentDurability--;

        if (currentDurability > 0)
        {
            SetBoxColor();
            if (scoreManager != null)
            {
                scoreManager.AddScore(10); // Add score for hitting the brick
            }
        }
        else
        {
            scoreManager.AddScore(maxDurability*100); // Add score when the brick is destroyed
            //audioManager.PlaySFX(audioManager.destroyBox);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(BonusPrefab, transform.position, transform.rotation);
    }
    void SetBoxColor()
    {
        if (currentDurability > 0 && currentDurability <= colors.Length)
        {
            rend.material.color = colors[currentDurability-1];
        }
    }
}
