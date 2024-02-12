using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class WaveSpawner : MonoBehaviour
{
    public List<Fish> fish = new List<Fish>();
    public List<GameObject> fishToSpawn = new List<GameObject>(); //poissons qui douivent apparaitre
    public List<GameObject> fishSpawned = new List<GameObject>(); 
    public Transform[] spawnPoints; //points de spawn pour le random
    
    public int currentWave; 
    public int waveValue; //valeur de spawn de la vague

    public Transform spawnLocation; //spawn point des poissons
    public int waveDuration; //durée d'un vague
    public float waveTimer;
    private float spawnInterval; //interval de temps entre l'apparition des poissons
    private float spawnTimer;

    public TextMeshProUGUI timerText;
    public int waveTimerInt;

    public TextMeshProUGUI waveNumber;

    public int equilibrageModifier = 5;

    public static WaveSpawner instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //StartCoroutine(WaitForStart()); //genere la premiere vague apres 3 secondes
        GenerateWave();
    }

    void FixedUpdate()
    {
        waveTimerInt = Mathf.RoundToInt(waveTimer); //Affichage du timer
        timerText.text = (waveTimerInt + " s");
        
        waveTimer -= Time.fixedDeltaTime;
        
        if (spawnTimer <= 0)
        {
            if (fishToSpawn.Count > 0)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                
                GameObject newFish = Instantiate(fishToSpawn[0], randomPoint.position, Quaternion.identity);
                fishToSpawn.RemoveAt(0);
                fishSpawned.Add(newFish); //maintenant je dois le remove quand il meurt fdp
                
                spawnTimer = spawnInterval; //reset du spawnTimer
            }
            /*else 
            {
                waveTimer = 0; //No fish left to spawn ==> le pb viens de là ? oui le probléme viens d'ici
            }*/
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }

        if (fishToSpawn.Count == 0 && fishSpawned.Count <= 0)
        {
            NextWave();
        }
        
        if (waveTimer <= 0 && fishSpawned.Count > 0)
        {
            Loose();
        }

        if (waveTimer <= 5f)
        {
            timerText.transform.DOScale(1.25f, 2f);
            //timerText.DOFade(255, 5f);
            timerText.DOColor(Color.red, 5f);
        }
        else
        {
            timerText.transform.DOScale(1f, 5f);
            timerText.DOColor(Color.white, 5f);
        }
    }

    public void NextWave() //lance la vague suivante
    { 
        waveNumber.text = (currentWave + "");
        currentWave += 1;
        //waveValue = (currentWave * 10) + equilibrageModifier;//trouver une formule pour l'équilibrage
        //waveDuration = currentWave * 10; 
        GenerateWave();
    }

    public void Loose()
    { 
        Debug.Log("t'as perdu"); //afficher l'écran de défaite

        UiAnimEndGame.instance.OpenMenu(); //pour ouvrir le menu avec une anim
    }

    public void GenerateWave() 
    {
        waveValue = currentWave * 15; //calcul de la valeur en poissons de la vague en fonction de leur cout
        GenerateFish();
        
        spawnInterval = waveDuration / fishToSpawn.Count; 
        
        waveDuration = (currentWave * 10) + equilibrageModifier; //set le timer sur la durée de la vague
        waveTimer = waveDuration;
    }

    public void GenerateFish() //permet de générer mes poissons
    {
        List<GameObject> generatedFish = new List<GameObject>();
        
        while (waveValue > 0)
        {
            int randFishId = Random.Range(0, fish.Count);
            int randFishCost = fish[randFishId].cost;

            if (waveValue - randFishCost >= 0)
            {
                generatedFish.Add(fish[randFishId].fishPrefab);
                waveValue -= randFishCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        fishToSpawn.Clear();
        fishToSpawn = generatedFish;
    }

    /*private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3f);
        GenerateWave();
    }*/
}

[System.Serializable]

public class Fish
{
    public GameObject fishPrefab; 
    public int cost; //cout des poissons
}
