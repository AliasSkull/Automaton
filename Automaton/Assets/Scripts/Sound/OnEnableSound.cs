using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSound : MonoBehaviour
{
    public AudioSource _as;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _as.time = 0;
        _as.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
