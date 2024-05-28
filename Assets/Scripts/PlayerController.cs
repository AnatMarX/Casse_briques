using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    //private GameObject go;
    //[Header("Public variables")]
    //public bool Rotate;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Player.transform.position.x > -8.13f)
            {
                Player.transform.Translate(speed * Vector3.up * Time.deltaTime * 10f);
            }
            else
            {
                // Si Player en butée
                Vector3 newPosition = Player.transform.position;
                newPosition.x = -8.13f;
                Player.transform.position = newPosition;
            }
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Player.transform.position.x < 8.09f)
            {
                Player.transform.Translate(speed * Vector3.down * Time.deltaTime * 10f);
            }
            else {
                // Si Player en butée
                Vector3 newPosition = Player.transform.position;
                newPosition.x = 8.09f;
                Player.transform.position = newPosition;
            }
        }
    }
}
