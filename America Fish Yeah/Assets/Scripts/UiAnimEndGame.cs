using UnityEngine;

public class UiAnimEndGame : MonoBehaviour
{
    public static UiAnimEndGame instance;
    
    public Animator anim;
    [SerializeField] public bool menuOpen = false;

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

    private void Update()
    {
        if (menuOpen == true)
        {
            Time.timeScale = 0; //Je ne sais pas pourquoi ca ne fonctionne pas ?
            Debug.Log("Le menu est ouvert");
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OpenMenu()
    {
        menuOpen = true;
        anim.SetBool("isEndGame", true);
        //waveNumber.text = ()
        //Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        anim.SetBool("isEndGame", false);
        Cursor.visible = false;
    }
}
