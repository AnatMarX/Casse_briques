using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Vitesse de d�placement de la boule
    private float ballSpeed = 25;

    // R�f�rence au Rigidbody
    private Rigidbody rb;
    private Vector3 direction;
    void Start()
    {
        // Obtenir le Rigidbody attach� � l'objet
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, ballSpeed, 0);
    }

    void FixedUpdate()
    {
        //Debug.Log(ballSpeed);
        //Debug.Log(rb.velocity.normalized);
        direction = rb.velocity;
        direction[2] = 0.0f;
        direction = direction.normalized;
        rb.velocity = direction * ballSpeed;
        rb.angularVelocity = Vector3.zero; // On emp�che la rotation pour �viter des blocages
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifier si la boule a touch� un obstacle
        if (collision.gameObject.tag == "Obstacle")
        {
            
        }
    }
}