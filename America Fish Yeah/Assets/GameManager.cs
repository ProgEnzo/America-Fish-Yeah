using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal new static GameManager instance;

    private bool isGamePaused = false;
    
    //Menus
    //[SerializeField] private GameObject gameOver;
    //[SerializeField] private GameObject win;
    [SerializeField] private GameObject pause;

    [SerializeField] private KeyCode onPause = KeyCode.Escape;

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
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Start()
    {
        //UiAnimPause.instance.CloseMenu();
    }

    private void Update()
    {
        PauseMenu();
        
        if (isGamePaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(onPause))
        {
            isGamePaused = true;
            UiAnimPause.instance.OpenMenu();
        }
    }

    public void Resume()
    {
        isGamePaused = false;
        UiAnimPause.instance.CloseMenu();
    }
    
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        //pause.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
