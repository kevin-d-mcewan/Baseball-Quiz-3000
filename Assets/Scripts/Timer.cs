using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    float timerValue;
    [SerializeField] float timeToCompleteQuestion = 10.0f;
    [SerializeField] float timeToShowCorrectAnswer = 3.0f;

    // should really create Getter and Setter instead of public
    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    
   
    void Update()
    {
        UpdateTimer();
    }

    /* Used for setting timer to 0 if answer is clicked on/submitted */
    public void CancelTimer()
    {
        timerValue = 0;
    }

    private void UpdateTimer()
    {
        
        // We will be shaving off some time with each amount of time
        timerValue -= Time.deltaTime;

        /* 'isAnsweringQuestion {iAQ} is true and timerValue > 0 then we will set fillFraction (timerSprite) = to % of time clock has remaining. 
         Otherwise, if 'iAQ' is false we will change the timerValue = to timeToShowCorrectAnswer*/
        if(isAnsweringQuestion)
        {
            if (timerValue > 0) 
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        /* if 'iAQ' is false but timerValue > 0 we will make the fillFraction (timerSprite) = to % of time remaining from amount of timeToShowCorrectAnswer.
         if 'iAQ' then becomes true we are going to load the next question and change timerValue = to time alloted for timeToCompleteAnswer*/
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }

            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }

        
    }

}
