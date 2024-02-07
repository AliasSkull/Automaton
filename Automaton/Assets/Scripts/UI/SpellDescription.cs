using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellDescription : MonoBehaviour
{
    private GameObject descriptionBox;
    
    // Start is called before the first frame update
    void Start()
    {
        descriptionBox = this.transform.GetChild(0).gameObject;
        descriptionBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PointerEnter()
    {
        descriptionBox.SetActive(true);
    }

    public void PointerExit()
    {
        
        descriptionBox.SetActive(false);
    }
}
