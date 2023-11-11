using Unity.Mathematics;
using UnityEngine;

public class target : MonoBehaviour
{
   public float health = 50f;
   public GameObject loot;
   public GameObject vfxDie;

   private AudioSource audio;

   //public static target instance;

   /*public void Awake()
   {
      instance = this;
   }*/

   private void Start()
   {
      audio = GetComponent<AudioSource>();
   }

   public void TakeDamage(float amount)
   {
      health -= amount;

      if (health <= 0f)
      {
         Die();
      }
   }

   void Die()
   {
      WaveSpawner.instance.fishSpawned.Remove(this.gameObject);
      audio.PlayOneShot(audio.clip);
      Instantiate(loot, transform.position, quaternion.identity);
      Instantiate(vfxDie, transform.position, quaternion.identity);
      Destroy(gameObject);
   }
}
