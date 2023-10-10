using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float destructionTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe", destructionTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
