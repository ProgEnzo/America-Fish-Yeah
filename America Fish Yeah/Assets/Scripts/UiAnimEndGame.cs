using UnityEngine;

public class UiAnimEndGame : MonoBehaviour
{
    public static UiAnimEndGame instance;
    
    public Animator anim;
    [SerializeField] private bool menuOpen = false;
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

    private void Update()
    {
        if (menuOpen)
        {
            Debug.Log("Le menu est ouvert");
            Time.timeScale = 0; //Je ne sais pas pourquoi ca ne fonctionne pas ?
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /*private void Start()
    {
        score = ScoreManager.instance.scoreText;
        waveNumber = WaveSpawner.instance.waveNumber;
    }*/

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
