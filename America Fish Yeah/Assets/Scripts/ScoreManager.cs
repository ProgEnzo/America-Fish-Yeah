using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;

    public int scoreToGive = 1;

    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = score.ToString() + "";
    }

    public void AddPoint()
    {
        score += scoreToGive;
        scoreText.text = score.ToString() + "";
    }
}
