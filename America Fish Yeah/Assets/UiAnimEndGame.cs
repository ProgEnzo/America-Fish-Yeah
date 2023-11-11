using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiAnimEndGame : MonoBehaviour
{
    public static UiAnimEndGame instance;
    
    public Animator anim;
    private bool menuOpen = false;
    //public GameObject endGameMenu;
    
    //public TextMeshProUGUI waveNumber;
    //public TextMeshProUGUI score;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    /*private void Start()
    {
        score = ScoreManager.instance.scoreText;
        waveNumber = WaveSpawner.instance.waveNumber;
    }*/

    public void OpenMenu()
    {
        anim.SetBool("isEndGame", true);
        //waveNumber.text = ()
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        anim.SetBool("isEndGame", false);
        Cursor.visible = false;
    }
}
