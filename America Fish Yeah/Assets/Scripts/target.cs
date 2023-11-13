using Unity.Mathematics;
using UnityEngine;

public class target : MonoBehaviour
{
   public float health = 50f;
   public GameObject loot;
   public GameObject vfxDie;

   //public static target instance;

   /*public void Awake()
   {
      instance = this;
   }*/

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
      Instantiate(loot, transform.position, quaternion.identity);
      GameObject mort = Instantiate(vfxDie, transform.position, quaternion.identity);
      Destroy(mort, 2f);
      Destroy(gameObject);
   }
}
