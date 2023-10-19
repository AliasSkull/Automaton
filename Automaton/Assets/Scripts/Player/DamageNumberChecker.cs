using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberChecker : MonoBehaviour
{
    public DynamicTextData enemyDamage;
    public DynamicTextData playerDamage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageTextShower1000(Vector3 pos, string damage, int damageType)
    {
        if(damageType == 1)
        {
            DynamicTextManager.CreateText(pos, damage, enemyDamage);
        }
        else if(damageType == 0)
        {
            DynamicTextManager.CreateText(pos, damage, playerDamage);
        }
        
        
    }


}
