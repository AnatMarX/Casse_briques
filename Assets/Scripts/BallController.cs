using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Vitesse de déplacement de la boule
    public float ballSpeed = 6;

    // Référence au Rigidbody
    private Rigidbody rb;
    private Vector3 direction;
    void Start()
    {
        // Obtenir le Rigidbody attaché à l'objet
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
        // Vérifier si la boule a touché un obstacle
        if (collision.gameObject.tag == "Obstacle")
        {
            
        }
    }
}