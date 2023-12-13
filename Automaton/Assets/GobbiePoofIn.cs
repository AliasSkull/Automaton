using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobbiePoofIn : MonoBehaviour
{
    public GameObject gobbiesParent;
    public GameObject gobPoof;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            gobbiesParent.SetActive(true);
            Transform l1Gob = gobbiesParent.transform.Find("GoblinsLevel1");
            foreach(Transform gob in l1Gob)
            {
                Instantiate(gobPoof, gob.transform.position, gob.transform.rotation);
            }
        }
    }
}
