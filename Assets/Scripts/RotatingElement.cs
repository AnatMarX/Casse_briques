using UnityEngine;

public class RotatingElement : MonoBehaviour
{
    public Vector3 rotationSpeed;
    public int fallSpeed;

    void Start()
    {
        // G�n�rer une vitesse de rotation al�atoire
        rotationSpeed = new Vector3(
            Random.Range(-50, 50),
            Random.Range(-50, 50),
            Random.Range(-50, 50)
        );

        fallSpeed = 10;

        // D�finir une couleur al�atoire
        SetRandomColor();
    }

    void Update()
    {
        // Appliquer la rotation
        transform.Rotate(rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // D�truire l'objet s'il tombe en dessous d'une certaine hauteur ou sort de l'�cran
        if (transform.position.y < -55 ||
            transform.position.z > 100 || transform.position.z < -100 ||
            transform.position.x > 100 || transform.position.x < -100)
        {
            Destroy(gameObject);
        }
    }

    void SetRandomColor()
    {
        // S�lectionner une couleur al�atoire parmi les options
        Color[] colors = { Color.blue, Color.Lerp(Color.blue, Color.magenta, 0.5f),
                           Color.magenta, Color.Lerp(Color.black, Color.magenta, 0.5f),
                           Color.black };
        Color randomColor = colors[Random.Range(0, colors.Length)];

        // Appliquer la couleur au composant de rendu
        GetComponent<Renderer>().material.color = randomColor;
    }
}
