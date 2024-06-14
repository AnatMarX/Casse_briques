using UnityEngine;

public class BallController : MonoBehaviour
{
    // Vitesse de d�placement de la boule
    private float ballSpeed = 0;
    private float impulseBallSpeed = 5;
    public LevelController levelController;
    // R�f�rence au go & au Rigidbody
    private GameObject go;
    private Rigidbody rb;
    private Vector3 direction;
    private bool impulse;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        this.impulse = false;
        // Obtenir le Rigidbody attach� � l'objet
        rb = GetComponent<Rigidbody>();
        go=getGo();
    }
    public void SetBallSpeed(float speed)
    {
        ballSpeed = speed;
    }

    public void SetImpulse()
    {
        this.impulse = false; // quand impulse = false, l'impulsion peut �tre d�clench�e
    }
    // Ref gameobject
    private GameObject getGo()
    {
        return this.gameObject;
    }

    void FixedUpdate()
    {
        //Debug.Log(rb.velocity); 
        
        
        if (go.transform.parent == null && this.impulse == false)
        {
            Debug.Log("impulsion");
            ballSpeed = impulseBallSpeed;
            impulse = true;
            rb.velocity = new Vector3(0, ballSpeed, 0);
        }
        
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
            levelController.KillPlayer();
        }
        if (collision.gameObject.tag == "Brick")
        {
            audioManager.PlaySFX(audioManager.touchBrick);
        }
        // Ajout d'une l�g�re d�viation al�atoire pour �viter le blocage de la balle
        AddRandomDeviation();
    }

    void AddRandomDeviation()
    {
        float deviationStrength = 0.1f; // Ajuste cette valeur pour contr�ler l'intensit� de la d�viation
        Vector3 randomDeviation = new Vector3(
            Random.Range(-deviationStrength, deviationStrength),
            Random.Range(-deviationStrength, deviationStrength),
            Random.Range(-deviationStrength, deviationStrength)
        );

        rb.velocity += randomDeviation;
    }
}