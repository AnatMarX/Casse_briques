using UnityEngine;

public class BallController : MonoBehaviour
{
    // Vitesse de déplacement de la boule
    private float ballSpeed = 0;
    private float impulseBallSpeed = 5;
    public LevelController levelController;
    // Référence au go & au Rigidbody
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
        // Obtenir le Rigidbody attaché à l'objet
        rb = GetComponent<Rigidbody>();
        go=getGo();
    }
    public void SetBallSpeed(float speed)
    {
        ballSpeed = speed;
    }

    public void SetImpulse()
    {
        this.impulse = false; // quand impulse = false, l'impulsion peut être déclenchée
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
        rb.angularVelocity = Vector3.zero; // On empêche la rotation pour éviter des blocages
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si la boule touche 
        if (collision.gameObject.tag == "Death")
        {
            audioManager.PlaySFX(audioManager.touchDeath);
            levelController.KillPlayer();
        }
        if (collision.gameObject.tag == "Brick")
        {
            audioManager.PlaySFX(audioManager.touchBrick);
        }
        // Ajout d'une légère déviation aléatoire pour éviter le blocage de la balle
        AddRandomDeviation();
    }

    void AddRandomDeviation()
    {
        float deviationStrength = 0.1f; // Ajuste cette valeur pour contrôler l'intensité de la déviation
        Vector3 randomDeviation = new Vector3(
            Random.Range(-deviationStrength, deviationStrength),
            Random.Range(-deviationStrength, deviationStrength),
            Random.Range(-deviationStrength, deviationStrength)
        );

        rb.velocity += randomDeviation;
    }
}