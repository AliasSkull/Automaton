using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float chainRange;
    public GameObject lightningChain;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChainLightningEffect(Transform hitEnemy)
    {
        Collider[] enemiesInProximity = Physics.OverlapSphere(hitEnemy.position, chainRange);
        foreach(Collider chainedEnemy in enemiesInProximity)
        {
            if(chainedEnemy.gameObject.tag == "Enemy" || chainedEnemy.gameObject.tag == "Damageable")
            {
                if(chainedEnemy.gameObject != hitEnemy.gameObject)
                {
                    print(chainedEnemy.gameObject);
                    Vector3 vectorBetween = new Vector3(hitEnemy.position.x, hitEnemy.position.y, hitEnemy.position.z) - new Vector3(chainedEnemy.transform.position.x, chainedEnemy.transform.position.y, chainedEnemy.transform.position.z);
                    float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
                    Quaternion rotationEnToEn = Quaternion.Euler(transform.rotation.x, rotation + 90, transform.rotation.z);

                    GameObject newChain = Instantiate(lightningChain, new Vector3((hitEnemy.position.x + chainedEnemy.transform.position.x) / 2, (hitEnemy.position.y + chainedEnemy.transform.position.y) / 2, (hitEnemy.position.z + chainedEnemy.transform.position.z) / 2), rotationEnToEn);
                    newChain.transform.localScale = new Vector3(newChain.transform.localScale.x, newChain.transform.localScale.y, vectorBetween.magnitude);
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Damageable")
        {
            ChainLightningEffect(other.transform);
        }
    }
}
