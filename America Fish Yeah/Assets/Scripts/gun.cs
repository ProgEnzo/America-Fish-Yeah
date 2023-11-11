using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 30f;

    public Animator recoil;

    public Camera fpsCam;

    private float nextTimeToFire = 1f;

    public Recoil Recoil_Script;

    //[SerializeField] private AudioClip shootingSound;
    //[SerializeField] private AudioClip impactSound; ==> trop de detail
    private AudioSource audioSource;

    public void Start()
    {
       // Recoil_Script = transform.Find("CameraRecoil").GetComponent<Recoil>();
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
            //audioSource.clip = shootingSound;
            //audioSource.spatialBlend = 1;
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            recoil.SetBool("isShooting", false);
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        
        recoil.SetBool("isShooting", true);
        
        Recoil_Script.RecoilFire(); //a chaque fois que shoot est appelé ca appel aussi recoil
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform);

            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        
    }
}
