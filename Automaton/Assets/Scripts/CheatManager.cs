using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("y") && Input.GetKey("u") && Input.GetKey("i"))
        {
            CheatCodes.CheatsOn = true;
            StaticValues.score = 3000000000;
        }
    }
}
