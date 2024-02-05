using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBlastSlow : MonoBehaviour
{
    public bool damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            Goblin gob = other.gameObject.GetComponent<Goblin>();
            if (!gob.stunned)
            {
                gob.StartCrowdControl(3, 3, this.transform.position, false);
            }

            if (damage)
            {
                gob.damageScript.TakeDamage(7, "");
            }
        }
    }
}
