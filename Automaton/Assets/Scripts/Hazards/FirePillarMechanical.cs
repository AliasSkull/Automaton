using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarMechanical : MonoBehaviour
{
    public GameObject pillars1;
    public GameObject pillars2;

    public float switchTime;
    
    // Start is called before the first frame update
    void Start()
    {
        pillars1.SetActive(false);
        InvokeRepeating("SwitchPillar", switchTime, switchTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPillar()
    {
        pillars1.SetActive(!pillars1.activeSelf);
        pillars2.SetActive(!pillars2.activeSelf);


    }

}
