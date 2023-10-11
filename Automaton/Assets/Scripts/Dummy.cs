using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage) 
    {
        Debug.Log(this.gameObject.name + "took damage");
        currentHealth = currentHealth - damage;
    }

    public void Death() 
    {
        Destroy(this.gameObject);
    }
}
