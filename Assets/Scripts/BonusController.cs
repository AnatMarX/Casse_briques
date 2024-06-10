using UnityEngine;

public class BonusController : MonoBehaviour
{
    float rotationSpeed = 300f; // Vitesse de rotation
    float moveSpeed = 1f; // Vitesse de descente
    float sizeChangeSpeed = 3f; // Vitesse de changement de taille
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Rotation continue
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Mouvement vers le bas
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);

        // Changement de taille entre normale et 50%
        float scaleChange = Mathf.PingPong(Time.time * sizeChangeSpeed, 1);
        transform.localScale = Vector3.Lerp(originalScale * 0.5f, originalScale, scaleChange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }
}

