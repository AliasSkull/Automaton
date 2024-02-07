using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAppearance : MonoBehaviour
{
    public float time;
    public GameObject objAppear;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObjectAppear(time));
    }

    public IEnumerator ObjectAppear(float time)
    {
        yield return new WaitForSeconds(time);
        objAppear.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
