using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    public LevelController LevelController;
    //private GameObject go;
    //[Header("Public variables")]
    //public bool Rotate;
    private float speed;
    //private bool roundStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ball = GameObject.Find("Ball");
            if (ball != null)
            {
                BallController ballController = ball.GetComponent<BallController>();
                if (ballController != null)
                {
                    ballController.transform.parent.DetachChildren();
                    //roundStarted = true;
                    LevelController.StartLevel();
                }
                else
                {
                    Debug.LogError("BallController component not found on Ball object.");
                }
            }
            else
            {
                Debug.LogError("Ball object not found.");
            }
        }

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
