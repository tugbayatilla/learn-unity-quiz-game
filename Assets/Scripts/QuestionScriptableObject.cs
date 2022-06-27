using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionScriptableObject : ScriptableObject
{
    [TextArea(2,4)]
    [SerializeField] private string question = "default question";

    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int correctAnswerIndex;

    public string getQuestion()
    {
        return question;
    }

    public int getCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string getAnswer(int index)
    {
        return answers[index];
    }

}
