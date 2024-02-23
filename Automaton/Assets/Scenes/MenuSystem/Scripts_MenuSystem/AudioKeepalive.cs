using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioKeepalive : MonoBehaviour
{
    public static AudioKeepalive inst = null;
    void Awake()
    {
        if( inst!=null && inst!=this ) Destroy(this.gameObject);
        else{
            inst=this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public static void Play(){
        if(inst==null) return;
        inst.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
