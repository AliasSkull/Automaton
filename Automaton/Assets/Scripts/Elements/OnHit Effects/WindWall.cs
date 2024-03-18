using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWall : MonoBehaviour
{
    private int extraDamage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Tornado")
        {
            extraDamage = (int)StaticValues.lDamageBuildup;

        }
        else if (pa.element2.name == "Tornado")
        {
            extraDamage = (int)StaticValues.rDamageBuildup;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(2, 0, this.transform.position, true);
                gob.damageScript.TakeDamage(5 + extraDamage, 7);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(2, 0, this.transform.position, true);
                rGob.damageScript.TakeDamage(5 + extraDamage, 7);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(2, 0, this.transform.position, true);
                srGob.damageScript.TakeDamage(5 + extraDamage, 7);
            }
        }
    }
}
