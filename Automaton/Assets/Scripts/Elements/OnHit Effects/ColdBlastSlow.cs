using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBlastSlow : MonoBehaviour
{
    public bool damage;
    private int extraDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Ice Blast")
        {
            extraDamage = (int)StaticValues.lDamageBuildup;

        }
        else if (pa.element2.name == "Ice Blast")
        {
            extraDamage = (int)StaticValues.rDamageBuildup;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                if (!gob.stunned)
                {
                    gob.StartCrowdControl(3, 3, this.transform.position, false);
                }

                if (damage)
                {
                    gob.damageScript.TakeDamage(20 + extraDamage, 6);
                }
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                if (!rGob.stunned)
                {
                    rGob.StartCrowdControl(3, 3, this.transform.position, false);
                }

                if (damage)
                {
                    rGob.damageScript.TakeDamage(20 + extraDamage, 6);
                }
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                if (!srGob.stunned)
                {
                    srGob.StartCrowdControl(3, 3, this.transform.position, false);
                }

                if (damage)
                {
                    srGob.damageScript.TakeDamage(20 + extraDamage, 6);
                }
            }
        }
    }
}
