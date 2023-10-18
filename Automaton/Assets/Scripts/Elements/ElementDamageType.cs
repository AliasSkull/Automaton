using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ElementDamageType : MonoBehaviour
{
    public int damageType;
    public Material newMat;
    public List<MeshRenderer> meshRenderers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDamageType(int dType, Material mat)
    {
        damageType = dType;
        newMat = mat;
        foreach(MeshRenderer mr in meshRenderers)
        {
            mr.materials[0].color = newMat.color;
        }
    }

    public void DealDamageApplyOnHit(GameObject enemyHit)
    {
        //find health script
        if (enemyHit.TryGetComponent<Enemy>(out Enemy enemyHPScript))
        {
            switch (damageType)
            {
                case 0:
                    enemyHPScript.TakeDamage(1);
                    
                    if (enemyHit.TryGetComponent<FireDot>(out FireDot fDOT))
                    {
                        Destroy(fDOT);
                        enemyHit.AddComponent<FireDot>();
                    }
                    else
                    {
                        enemyHit.AddComponent<FireDot>();
                    }
                        
                    break;
                case 1:
                    enemyHPScript.TakeDamage(3);
                    if (!enemyHit.TryGetComponent<WaterPushback>(out WaterPushback wpb))
                    {
                        enemyHit.AddComponent<WaterPushback>();
                    }
                    break;
                case 2:
                    enemyHPScript.TakeDamage(1f);
                    if(!enemyHit.TryGetComponent<LightningStun>(out LightningStun ls))
                    {
                        enemyHit.AddComponent<LightningStun>();
                    }
                    break;
            }
        }

        //deal damage

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Damageable")
        {
            DealDamageApplyOnHit(other.gameObject);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
        //if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Damageable")
        //{
         //   DealDamageApplyOnHit(other.gameObject);
        //}
    //}
}
