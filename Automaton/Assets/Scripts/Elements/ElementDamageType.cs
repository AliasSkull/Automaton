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
        if (enemyHit.TryGetComponent<Damageable>(out Damageable enemyHPScript))
        {
            switch (damageType)
            {
                case 0:
                    enemyHPScript.TakeDamage(2, "");
                    
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
                    enemyHPScript.TakeDamage(5, "Push ");
                    if (!enemyHit.TryGetComponent<WaterPushback>(out WaterPushback wpb))
                    {
                        enemyHit.AddComponent<WaterPushback>();
                    }
                    break;
                case 2:
                    enemyHPScript.TakeDamage(2, "Stun ");
                    break;
                case 3:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
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
