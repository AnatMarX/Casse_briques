using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Vitesse de déplacement de la boule
    public float ballSpeed = 0;

    public LevelController LevelController;
    // Référence au go & au Rigidbody
    private GameObject go;
    private Rigidbody rb;
    private Vector3 direction;
    private bool impulse;

    void Start()
    {
        impulse = false;
        // Obtenir le Rigidbody attaché à l'objet
        rb = GetComponent<Rigidbody>();
        go=getGo();
    }

    // Ref gameobject
    private GameObject getGo()
    {
        return this.gameObject;
    }
    private void Update()
    { 
        
    }

    void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        
        
        if (go.transform.parent == null && this.impulse == false)
        {
            Debug.Log("impulsion");
            this.ballSpeed = 5;
            this.impulse = true;
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

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si la boule touche 
        if (collision.gameObject.tag == "Death")
        {
            LevelController.Death();
        }
        if (collision.gameObject.tag == "Brick")
        {
            this.GetComponent<AudioSource>().Play();
            Debug.Log("PLayed Sound!");

        }
    }
}