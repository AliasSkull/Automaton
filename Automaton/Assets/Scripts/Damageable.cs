using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damageCount;

    public float damageTime;
    public float currentDamageTime;

    public GameObject Goblin;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageCount > 0)
        {
            DamageCounter();
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        damageCount = damageCount + 1;
        currentDamageTime = 0;

    
    }

    public void DamageCounter() 
    {
        currentDamageTime += Time.deltaTime;

        if (currentDamageTime == damageTime)
        {
            damageCount = 0;
            currentDamageTime = 0;
        }
    }

  

}
