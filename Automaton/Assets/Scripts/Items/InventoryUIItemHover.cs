using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIItemHover : MonoBehaviour
{
    public GameObject descPanel;

    private void Start()
    {
        //descPanel = transform.Find("DiscriptionPanel").gameObject;
        descPanel.SetActive(false);
    }

    public void MouseOver()
    {
        descPanel.SetActive(true);
        descPanel.transform.position = this.transform.position;
        //print("bruh");
    }

    public void MouseExit()
    {
        descPanel.SetActive(false);
    }
}
