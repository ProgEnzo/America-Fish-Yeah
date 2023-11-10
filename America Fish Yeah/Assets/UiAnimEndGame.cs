using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimEndGame : MonoBehaviour
{
    public static UiAnimEndGame instance;
    
    public Animator anim;
    private bool menuOpen = false;
    public GameObject endGameMenu;

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

    public void OpenMenu()
    {
        anim.SetBool("isEndGame", true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        anim.SetBool("isEndGame", false);
        Cursor.visible = false;
    }
}
