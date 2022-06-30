using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private ScoreKeeper scoreKeeper;
    
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = $"Congratulations\nYou scored {scoreKeeper.CalculateScore()}%";
    }
}
