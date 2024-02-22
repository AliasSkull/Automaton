using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepEnemy : MonoBehaviour
{
    public AudioSource _as;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstep()
    {
        _as.time = 0;
        _as.Play();
    }
}
