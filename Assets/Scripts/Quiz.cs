using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Questions")] [SerializeField]
    private List<QuestionScriptableObject> questions = new List<QuestionScriptableObject>();

    [SerializeField] private TextMeshProUGUI questionTitle;
    private QuestionScriptableObject currentQuestion;

    [Header("Answers")] [SerializeField] private GameObject[] answerButtons;
    private int correctAnswerIndex;
    private bool hasAnsweredEarly=true;

    [Header("Button Colors")] [SerializeField]
    private Sprite defaultAnswerSprite;

    [SerializeField] private Sprite correctAnswerSprite;

    [Header("Timers")] [SerializeField] private Image timerImage;
    private Timer timer;

    [Header("Scoring")] [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    [Header("Progress Bar")] 
    [SerializeField] private Slider progressBar;

    public bool isComplete;
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void DisplayQuestion()
    {
        questionTitle.text = currentQuestion.getQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI answerButton = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerButton.text = currentQuestion.getAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetButtonState(false);
        timer.CancelTimer();

        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}%";
    }

    private void DisplayAnswer(int index)
    {
        if (index == currentQuestion.getCorrectAnswerIndex())
        {
            questionTitle.text = "Correct!";
            var buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.incrementCorrectAnswers();
        }
        else
        {
            var correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
            string correctAnswer = currentQuestion.getAnswer(correctAnswerIndex);
            questionTitle.text = "Sorry, correct answer was, -> " + correctAnswer;
            var buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
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
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.incrementQuestionSeen();
            progressBar.value++;
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
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