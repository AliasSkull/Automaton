using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;

        Debug.Log("Ow");

        if (currentHealth <= 0)
        { 
        
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }


}
