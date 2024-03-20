using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCrushSecond : MonoBehaviour
{
    private int extraDamage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Wind Crush")
        {
            extraDamage = (int)StaticValues.lDamageBuildup;

        }
        else if (pa.element2.name == "Wind Crush")
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
        if (other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(2, 0, this.transform.position, false);
                gob.damageScript.TakeDamage(5 + extraDamage, 10);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(2, 0, this.transform.position, false);
                gob.damageScript.TakeDamage(5 + extraDamage, 10);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(2, 0, this.transform.position, false);
                gob.damageScript.TakeDamage(5 + extraDamage, 10);
            }
        }
    }
}
