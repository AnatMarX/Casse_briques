using UnityEngine;

public class BonusBallController : MonoBehaviour
{
    // Vitesse de d�placement de la boule
    private float ballSpeed = 0;

    private LevelController levelController;
    // R�f�rence au Rigidbody
    private Rigidbody rb;
    private Vector3 direction;

    AudioManager audioManager;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Obtenir le Rigidbody attach� � l'objet
        rb = GetComponent<Rigidbody>();
    }

    public void SetBallSpeed(float speed)
    {
        ballSpeed = speed;
    }

    public void Impulse(Vector3 direction)
    {
        Debug.Log("Bonus impulsion");
        ballSpeed = 8;

        if (rb == null)
        {
            Debug.LogError("Rigidbody n'est pas assign� !");
            return;
        }

        rb.velocity = direction * ballSpeed;
    }


void FixedUpdate()
    {
        //Debug.Log(ballSpeed);
        //aDebug.Log(rb.velocity.normalized);
        direction = rb.velocity;
        direction[2] = 0.0f;
        direction = direction.normalized;
        rb.velocity = direction * ballSpeed;
        rb.angularVelocity = Vector3.zero; // On emp�che la rotation pour �viter des blocages
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifier si la boule touche 
        if (collision.gameObject.tag == "Death")
        {
            audioManager.PlaySFX(audioManager.touchDeath);
            DestroyBall();
        }
        if (collision.gameObject.tag == "Brick")
        {
            audioManager.PlaySFX(audioManager.touchBrick);
        }
    }
}