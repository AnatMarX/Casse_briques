using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Vitesse de d�placement de la boule
    public float ballSpeed = 6;

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
        Debug.Log(ballSpeed);
        Debug.Log(rb.velocity.normalized);
        direction = rb.velocity.normalized;
        rb.velocity = direction * ballSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifier si la boule a touch� un obstacle
        if (collision.gameObject.tag == "Obstacle")
        {
            
        }
    }
}