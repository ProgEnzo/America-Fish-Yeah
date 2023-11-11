using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    [Header("Explosion Prefabs")] 
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private Vector3 explosionParticleOffset;
    [SerializeField] private GameObject audioSourcePrefab;

    [Header("Explosion Settings")] 
    [SerializeField] private float explosionDelay = 3f; 
    [SerializeField] private float explosionForce = 700f; 
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionDamage = 50f; //comment appliquer des degats

    [Header("Audio Effects")] 
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip impactSound;
    
    private float countDown;
    private bool hasExploded = false;
    private AudioSource audioSource;

    private void Start()
    {
        countDown = explosionDelay;
        audioSource = GetComponent<AudioSource>();
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
        
        CameraShake.Shake(0.5f, 0.5f);

        PlaySoundAtPosition(explosionSound);
        
        NearbyForceApply();
        
        Destroy(gameObject);
    }

    void PlaySoundAtPosition(AudioClip clip)
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource instantiatedAudioSource = audioSourceObject.GetComponent<AudioSource>();
        instantiatedAudioSource.clip = clip;
        instantiatedAudioSource.spatialBlend = 1; //c'est pour faire un son 3d incroyable j'apprend c'est fou
        instantiatedAudioSource.Play();
        
        Destroy(audioSource, instantiatedAudioSource.clip.length);
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
                //target.instance.TakeDamage(explosionDamage); //comment faire pour que ca ne soit que dans le rayon d'explosion ?
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        audioSource.clip = impactSound;
        audioSource.spatialBlend = 1;
        audioSource.Play();
    }
}
