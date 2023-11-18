using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPushback : MonoBehaviour
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
            Goblin gob = other.gameObject.GetComponent<Goblin>();
            gob.StartCrowdControl(2, 0, this.transform.position);
            gob.damageScript.TakeDamage(0, "Push");
        }
    }
}
