using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] private QuestionScriptableObject question;
    [SerializeField] private TextMeshProUGUI questionTitle;
    [SerializeField] private GameObject[] answerButtons;

    private int correctAnswerIndex;
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;
    void Start()
    {
        GetNextQuestion();
        //DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        questionTitle.text = question.getQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI answerButton = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerButton.text = question.getAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        if (index == question.getCorrectAnswerIndex())
        {
            questionTitle.text = "Correct!";
            var buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            var correctAnswerIndex = question.getCorrectAnswerIndex();
            string correctAnswer = question.getAnswer(correctAnswerIndex);
            questionTitle.text = "Sorry, correct answer was, -> " + correctAnswer;
            var buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
    }

    void SetButtonState(bool state)
    {
        foreach (var t in answerButtons)
        {
            Button button = t.GetComponent<Button>();
            button.interactable = state;
        }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    private void SetDefaultButtonSprites()
    {   
        foreach (var t in answerButtons)
        {
            var buttonImage = t.GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
