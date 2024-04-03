using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Input.GetKey("h") && Input.GetKey("e") && Input.GetKey("l") && Input.GetKey("p"))
        {
            SceneManager.LoadScene("IntroOpening");
        }
    }
}
