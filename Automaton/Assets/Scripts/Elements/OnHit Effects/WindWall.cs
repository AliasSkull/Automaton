using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWall : MonoBehaviour
{
    
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
                gob.StartCrowdControl(2, 0, this.transform.position, true);
                gob.damageScript.TakeDamage(0, 7);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(2, 0, this.transform.position, true);
                rGob.damageScript.TakeDamage(0, 7);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(2, 0, this.transform.position, true);
                srGob.damageScript.TakeDamage(0, 7);
            }
        }

        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && other.gameObject.name == "Dummy(Clone)")
        {
            other.gameObject.GetComponent<MeleeDummy>().Push(this.transform.position, true);
        }
    }
}
