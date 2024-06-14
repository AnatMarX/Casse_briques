using UnityEngine;

public class BonusController : MonoBehaviour
{
    float rotationSpeed = 300f; // Vitesse de rotation
    float moveSpeed = 1f; // Vitesse de descente
    float sizeChangeSpeed = 3f; // Vitesse de changement de taille
    private Vector3 originalScale;
    public GameObject bonusBallPrefab; // Prefab des balles bonus
    private float radius = 1f; // Radius around the Ball to spawn BonusBalls
    private GameObject ball;
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
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
            if (ball != null)
            {
                // Get the position of the ball
                Vector3 ballPosition = ball.transform.position;

                // Calculate the directions to spawn the BonusBalls
                for (int i = 0; i < 8; i++)
                {
                    float angle = i * Mathf.PI * 2f / 8;
                    Vector3 spawnDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                    Vector3 spawnPosition = ballPosition + spawnDirection * radius;

                    // Instantiate the BonusBall
                    GameObject bonusBall = Instantiate(bonusBallPrefab, spawnPosition, Quaternion.identity);

                    // Calculate the impulse direction (opposite to the direction from the ball to the BonusBall)
                    Vector3 impulseDirection = spawnPosition - ballPosition;

                    // Call the Impulse function on the BonusBall
                    bonusBall.GetComponent<BonusBallController>().Impulse(impulseDirection);
                }
            }
            else
            {
                Debug.LogError("No GameObject found with the tag 'Ball'.");
            }
        }
        else if (other.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }
}

