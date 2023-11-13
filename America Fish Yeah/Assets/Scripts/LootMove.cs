using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LootMove : MonoBehaviour
{
    private Loot lootScript;
    public Transform tpPos;
    //public GameObject dropLoot;

    private AudioSource audio;

    private void Start()
    {
        lootScript = gameObject.GetComponent<Loot>();
        tpPos = GameObject.FindGameObjectWithTag("tpPos").transform;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, lootScript.wallTransform.position, lootScript.moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WallCollider")
        {
            //Destroy(gameObject); //comment tp uniquement le poisson en haut
            //Instantiate(dropLoot, tpPos.transform.position, quaternion.identity);
            gameObject.transform.position = tpPos.transform.position;
            ScoreManager.instance.AddPoint();
            ScoreAffichage.instance.AddPoint();
            audio.PlayOneShot(audio.clip);
            Destroy(audio, 2f);
        }
    }
}
