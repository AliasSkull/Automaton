using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{

    public float damage;
    public Material newMat;
    public List<MeshRenderer> meshRenderers;

    public void SetDamageAmount(float damageAmount, Material mat)
    {
        damage = damageAmount;
        newMat = mat;
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.materials[0].color = newMat.color;
        }
    }

    public void DealDamage(GameObject enemyHit)
    {
        if (enemyHit.TryGetComponent<Damageable>(out Damageable enemyHPScript))
        {
            enemyHPScript.TakeDamage(damage, 102004812);
        }
    }
}
