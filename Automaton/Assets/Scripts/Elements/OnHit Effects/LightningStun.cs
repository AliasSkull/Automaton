using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Stun");
        StartCoroutine(TimedDestruction());
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
