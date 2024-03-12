using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLightningBlast : MonoBehaviour
{
    public bool notHIT = false;

    private Damageable[] dam = new Damageable[0];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!notHIT && dam.Length > 0)
        {
            foreach(Damageable d in dam)
            {
                d.TakeDamage(4, 12);
            }

            notHIT = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            dam = other.gameObject.GetComponents<Damageable>();
        }
    }
}
