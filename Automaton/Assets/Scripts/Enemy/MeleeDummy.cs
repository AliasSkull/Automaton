using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDummy : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxhealth;
    [SerializeField]
    private float damageCount;


    public float damageTime;
    public float currentDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void TakeDamage(float damage, string extraText) 
    {
        health = health - damage;
        damageCount = damageCount + 1;
        currentDamageTime = 0;
        if (damage > 0)
        {
            GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot"), extraText + damage.ToString(), 1);
        
        }
        else if (damage == 0)
        {
            GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot"), extraText, 1);
            Death();
        }


    }


    public void Death()
    {
        FindAnyObjectByType<DummySpawn>().DummyList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
