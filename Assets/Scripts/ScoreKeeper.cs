using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionsSeen = 0;

    public int getCorrectAnswers()
    {
        return correctAnswers;
    }
    public void incrementCorrectAnswers()
    {
        correctAnswers++;
    }
    public int getQuestionsSeen()
    {
        return questionsSeen;
    }
    public void incrementQuestionSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float) questionsSeen * 100);
    }
}
