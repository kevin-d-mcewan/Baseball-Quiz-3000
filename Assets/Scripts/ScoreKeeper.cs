using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int correctAnswers = 0;
    int questionsSeen = 0;


    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        // We need to cast 1 var to a float in order to get a decimal answer. Then we want to round that to the nearest whole number after its been calculated which is
        // why we used 'Mathf.RoundToInt'
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
