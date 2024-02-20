using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
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
                gob.StartCrowdControl(1, 3f, this.transform.position, false);
                if (gob.pushedBack)
                {
                    gob.damageScript.TakeDamage(10, "");
                }
                else
                {
                    gob.damageScript.TakeDamage(5, "");
                }
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(1, 3f, this.transform.position, false);
                if (rGob.pushedBack)
                {
                    rGob.damageScript.TakeDamage(10, "");
                }
                else
                {
                    rGob.damageScript.TakeDamage(5, "");
                }
            }
        }
    }
}
