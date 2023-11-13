using UnityEngine;

public class Appat : MonoBehaviour
{
    public static Appat instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }
    
    
}
