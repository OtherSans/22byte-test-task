using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private SceneController sceneController;
    private int totalScore;
    private int currentScore;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        var totalCrystalsAmount = GameObject.FindGameObjectsWithTag("Crystal");
        totalScore = totalCrystalsAmount.Length;
        currentScore = 0;
    }
    public void ApplyScore()
    {
        currentScore++;
    }
    private void Update()
    {
        scoreText.text = currentScore.ToString() + "/" + totalScore.ToString();
        if(currentScore >= totalScore)
        {
            sceneController.LoadScene("YouWonScene");
        }
    }
}
