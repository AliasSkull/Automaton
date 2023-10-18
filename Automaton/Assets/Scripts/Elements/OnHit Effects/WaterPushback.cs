using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPushback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("pushback");
        StartCoroutine(TimedDestruction());
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
