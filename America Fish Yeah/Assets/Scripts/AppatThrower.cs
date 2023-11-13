using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AppatThrower : MonoBehaviour
{
    [Header("Grenade Prefab")] 
    [SerializeField] private GameObject appatPrefab;

    [Header("Grenade Settings")] 
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse1;
    [SerializeField] private Transform throwPosition;
    [SerializeField] private Vector3 throwDirection = new Vector3(0, 1, 0);

    [Header("Grenade Force")] 
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float maxForce = 20f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip throwSound;

    [SerializeField] private LineRenderer trajectoryLine;

    private bool isCharging = false;
    private float chargeTime = 0f;
    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            StartThrowing();
        }

        if (isCharging)
        {
            ChargeThrow();
        }

        if (Input.GetKeyUp(throwKey))
        {
            ReleaseThrow();
        }
    }

    void StartThrowing()
    {
        isCharging = true;
        chargeTime = 0f;

        trajectoryLine.enabled = true;
    }

    void ChargeThrow()
    {
        chargeTime += Time.deltaTime;

        Vector3 grenadeVelocity = (mainCamera.transform.forward + throwDirection).normalized *
                                  Mathf.Min(chargeTime * throwForce, maxForce);
        ShowTrajectory(throwPosition.position + throwPosition.forward, grenadeVelocity);
    }

    void ReleaseThrow()
    {
        ThrowAppat(Mathf.Min(chargeTime * throwForce, maxForce));
        isCharging = false;

        trajectoryLine.enabled = false;
    }

    void ThrowAppat(float force)
    {
        Vector3 spawnPosition = throwPosition.position; // + mainCam.transform.rotation ?

        GameObject appat = Instantiate(appatPrefab, spawnPosition, quaternion.identity); //anciennement mainCam.Transform.Rotation

        Rigidbody rb = appat.GetComponent<Rigidbody>();

        Vector3 finalThrowDirection = (mainCamera.transform.forward + throwDirection).normalized;
        rb.AddForce(finalThrowDirection * force, ForceMode.VelocityChange);
        
        GrenadeAudioManager.instance.PlayOneShot(throwSound, 0.5f);
        
        Destroy(appat, 7f);
    }

    void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        trajectoryLine.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + 0.5f * Physics.gravity * time * time;
        }
        trajectoryLine.SetPositions(points);
    }
}
