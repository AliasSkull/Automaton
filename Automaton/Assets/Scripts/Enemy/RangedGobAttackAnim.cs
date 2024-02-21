using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGobAttackAnim : MonoBehaviour
{

    public RangeGoblin rg;
    public SpecialRangedGoblin srg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAttack()
    {
        if(rg != null)
        {
            rg.isAttacking = false;
            rg.isWalking = true;
        }
        else if(srg != null)
        {
            srg.isAttacking = false;
            srg.isWalking = true;
        }
        

    }

    public void Attack()
    {
        if (rg != null)
        {
            rg.CreateProjectile();
        }
        else if (srg != null)
        {
            srg.CreateProjectile();
        }
    }
}
