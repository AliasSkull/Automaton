using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFindCamera : MonoBehaviour
{
    public Canvas canv;
    public Camera mainCam;
    
    // Start is called before the first frame update
    void Start()
    {
        //mainCam = GameObject.Find("Player").transform.Find("Main Camera").gameObject.GetComponent<Camera>();
        mainCam = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>();
        canv = GetComponent<Canvas>();

        canv.worldCamera = mainCam;

        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
