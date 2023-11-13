using UnityEngine;

public class UiAnimPause : MonoBehaviour
{
    public static UiAnimPause instance;
    
    public Animator anim;
    private bool menuOpen = false;
    public GameObject pauseMenu;

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
        anim.SetBool("isPauseOpen", true);
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        anim.SetBool("isPauseOpen", false);
        Cursor.visible = false;
    }
}
