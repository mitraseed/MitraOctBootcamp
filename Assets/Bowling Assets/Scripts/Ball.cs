using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //check for collision with any object
        //Debug.Log("Ball has collided with" + collision.gameObject.name)

        if (collision.gameObject.CompareTag("Pin"))
        {
            Debug.Log("The object we collided with is " + collision.gameObject.name);

        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pit"))
        {
            gameManager.SetNextThrow();

            //Destrpy Ball GameObject
            Destroy(gameObject);
        }

       
    }
}