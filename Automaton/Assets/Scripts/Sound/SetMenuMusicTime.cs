using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuMusicTime : MonoBehaviour
{
    public AudioSource _as;

    public bool menu;
    
    // Start is called before the first frame update
    void Start()
    {
        if (menu)
        {
            _as.time = StaticValues.menuSoundTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu)
        {
            StaticValues.menuSoundTime = _as.time;
        }
        

    }
}
