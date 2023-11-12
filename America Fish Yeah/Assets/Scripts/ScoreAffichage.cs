using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAffichage : MonoBehaviour
{
    //public TextMeshProUGUI waveNumber;
    
    public int scoreToGive = 1;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveNumber;
    
    private int score = 0;

    public static ScoreAffichage instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = score.ToString() + "";
    }

    private void Update()
    {
        waveNumber.text = WaveSpawner.instance.currentWave.ToString() + " manches réussies";
    }

    public void AddPoint()
    {
        score += scoreToGive;
        scoreText.text = score.ToString() + " poissons pêchés à l'Américaine bravo";
    }
}
