using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMovementSpeed = 1.0f;
    public float arrowMinPosition = -0.25f;
    public float arrowMaxPosition = 0.25f;
    public Transform throwingArrow;
    public Transform ballSpawnPoint;
    public float throwForce = 5.0f;
    public Animator throwingArrowAnim;

    public Rigidbody[] balls;

    private float horizontalInput;
    private Vector3 ballOffset;
    private bool wasBallThrown;
    private Rigidbody selectedBall;

    // Start is called before the first frame update
    void Start()
    {
        ballOffset = ballSpawnPoint.position - throwingArrow.position;
        
        //StartThrow();
    }

    public void StartThrow()
    {
        throwingArrowAnim.SetBool("Aiming", true);
        wasBallThrown = false;

        //Spawn a New Ball When Start Throw is Called
        int randomNumber = GetRandomNumber(0, balls.Length);
        selectedBall = Instantiate(balls[randomNumber], ballSpawnPoint.position, Quaternion.identity);
    }

    private int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrowWithConstraints();
        TryThrowBall();
    }


    //Moving without Constraints
    private void MoveArrowWithoutConstraints()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        throwingArrow.position += throwingArrow.transform.right * horizontalInput * playerMovementSpeed * Time.deltaTime;
    }


    
    private void MoveArrowWithConstraints()
    {
        // Checks if ball has not yet been thrown
        if(!wasBallThrown) //wasBallThrown == false
        {
            //Moving with Constraints
            float movePosition = Input.GetAxis("Horizontal") * playerMovementSpeed * Time.deltaTime;
            throwingArrow.position = new Vector3(
                Mathf.Clamp(throwingArrow.transform.position.x + movePosition, arrowMinPosition, arrowMaxPosition),
                throwingArrow.position.y,
                throwingArrow.position.z

                );

            //Set Ball Position based on throwing direction position
            selectedBall.transform.position = throwingArrow.position + ballOffset;



        }


    }

    private void TryThrowBall()
    {
        //Throw the ball
        if(Input.GetKeyDown(KeyCode.Space))
        {
            wasBallThrown = true;
            selectedBall.AddForce(throwingArrow.forward * throwForce, ForceMode.Impulse);
            throwingArrowAnim.SetBool("Aiming", false);

        }


    }
}

