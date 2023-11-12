using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Transform wallTransform;
    public float moveSpeed = 17f;

    private LootMove lootMoveScript;

    private void Start()
    {
        wallTransform = GameObject.FindGameObjectWithTag("WallCollider").transform;
        lootMoveScript = gameObject.GetComponent<LootMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LootDetector")
        {
            lootMoveScript.enabled = true;
            //StartCoroutine(WaitToDisable());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LootDetector")
        {
            lootMoveScript.enabled = false;
        }
    }

    /*private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(3f); //tres tres shlag
        lootMoveScript.enabled = false;
    }*/
}
