using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGobAttackAnim : MonoBehaviour
{

    public RangeGoblin rg;

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
        rg.isAttacking = false;
        rg.isWalking = true;
    }

    public void Attack()
    {
        rg.CreateProjectile();
    }
}
