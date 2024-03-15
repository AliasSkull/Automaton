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
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                if (!gob.stunned)
                {
                    gob.StartCrowdControl(3, 3, this.transform.position, false);
                }

                if (damage)
                {
                    gob.damageScript.TakeDamage(20, 6);
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
                    rGob.damageScript.TakeDamage(20, 6);
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
                    srGob.damageScript.TakeDamage(20, 6);
                }
            }
        }

        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && other.gameObject.name == "Dummy(Clone)")
        {
            other.gameObject.transform.Find("Hurtbox").gameObject.GetComponent<Damageable>().TakeDamage(20, 1);
        }
    }
}
