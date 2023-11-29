using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUIManagement : MonoBehaviour
{
    public List<GameObject> Hearts;

    private int totalHealth;
    private int currentHealth;
    private int prevHP;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform heart in transform)
        {
            Hearts.Add(heart.transform.GetChild(0).gameObject);
            Hearts.Add(heart.gameObject);
        }

        totalHealth = Hearts.Count * 2;
        currentHealth = totalHealth;
        prevHP = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth != prevHP)
        {
            RedrawHeartUI();
            prevHP = currentHealth;
        }
    }

    public void RedrawHeartUI()
    {
        for (int i = 0; i < totalHealth - currentHealth; i++)
        {
            Hearts[i].SetActive(false);
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
    }
}
