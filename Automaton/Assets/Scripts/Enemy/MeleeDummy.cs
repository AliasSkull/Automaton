using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDummy : MonoBehaviour
{
    public Damageable damage;

    // Start is called before the first frame update
    void Start()
    {
      
      
    }

    // Update is called once per frame
    void Update()
    {
        if (damage.currentHealth == 0)
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
