using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    [Header("Explosion Prefabs")] 
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private Vector3 explosionParticleOffset;

    [Header("Explosion Settings")] 
    [SerializeField] private float explosionDelay = 3f; 
    [SerializeField] private float explosionForce = 700f; 
    [SerializeField] private float explosionRadius = 5f;

    [Header("Audio Effects")] 
    
    private float countDown;
    private bool hasExploded = false;

    private void Start()
    {
        countDown = explosionDelay;
    }

    private void Update()
    {
        if (!hasExploded)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0f)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    void Explode()
    {
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        
        Destroy(explosionEffect, 4f);
        
        //Play sfx
        
        NearbyForceApply();
        
        Destroy(gameObject);
    }

    void NearbyForceApply()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius); //AddExplosionForce permet d'ajouter une force plus exact pour une explosion pas besoin de le simuler nous meme avec un addForce
            }
        }
    }
}
