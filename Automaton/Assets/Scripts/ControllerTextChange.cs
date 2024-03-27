using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerTextChange : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    public string noControllerText;
    public string controllerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticValues.controller)
        {
            text.text = controllerText;
        }
        else
        {
            text.text = noControllerText;
        }
    }
}
