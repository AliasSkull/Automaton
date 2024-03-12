using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDummy : MonoBehaviour
{
    private int health;
    private int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        FindAnyObjectByType<DummySpawn>().DummyList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
