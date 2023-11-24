using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;


    private int totalScore;

    public int currentThrow { get; private set; }

    public int currentFrame { get; private set; }


    private int[] frames = new int[10];

    private bool isSpare = false;
    private bool isStrike = false;


   private void Start()
    {

        ResetScore();
    }


    //Set value for our frame score each time we throw the ball
    public void SetFrameScore(int score)
    {
        //Ball 1
        if(currentThrow == 1)
        {
            frames[currentFrame - 1] += score; // setting the right frame index and addind the score value from the parameter passed

            //parallel process to chek spare
            if(isSpare)
            {
                frames[currentFrame - 2] += score;
                isSpare = false;

            }
            //----------------
            if(score == 10)
            {
                if(currentFrame == 10)
                {
                    currentThrow++; //wait for ball 2
                }

                else
                {
                    isStrike = true;
                    currentFrame++; // move to next frame since full marks obtained
                }

                //Resel All pins via GameManager
                gameManager.ResetAllPins();



            }

            else
            {
                currentThrow++; //wait for ball 2
            }

            return;
        }

        //ball 2
        if (currentThrow == 2)
        {
            frames[currentFrame] += score;

            //parallel process to chek strike
            if (isStrike)
            {
                frames[currentFrame - 2] += frames[currentFrame - 1];
                isStrike = false;
            }
            //------------------------------------------

            if(frames[currentFrame -1] == 10) // is tital frame score is 10?
            {
                if(currentFrame == 10)
                {
                    currentThrow++; // wait for ball 3
                }
                else
                {
                    isSpare = true;
                    currentThrow++;
                    currentFrame = 1;
                }

               
            }
            else
            {
                if(currentFrame == 10)
                {
                    //end of all throws
                    currentThrow = 0;
                    currentFrame = 0;

                }
                else
                {
                    currentFrame++;
                    currentThrow = 1;
                }
            }

            //Resel All pins via GameManager
            gameManager.ResetAllPins();

            return;
        }

        // Ball 3 only happen in frame 10
        if(currentThrow == 3 && currentFrame == 10)
        {
            frames[currentFrame - 1] += score;

            //end of all throw
            currentThrow = 0;
            currentFrame = 0;

            return;
        }
    }



    public int CalculateTotalScore()
    {
        totalScore = 0;
        foreach(var frame in frames)
        {
            totalScore += frame;
        }

        return totalScore;
    }


    private void ResetScore()
    {
        totalScore = 0;
        currentFrame = 1;
        currentThrow = 1;
        frames = new int[10];
    }


 

    // Update is called once per frame
    void Update()
    {
        
    }
}
