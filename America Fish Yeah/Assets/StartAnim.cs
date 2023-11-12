using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isPlaying", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
