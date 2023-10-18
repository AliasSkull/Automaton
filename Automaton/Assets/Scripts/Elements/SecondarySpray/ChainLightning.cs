using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void ChainLightningEffect(Transform hitEnemyHurtbox, Transform hitEnemySprite)
    {
        Collider[] enemiesInProximity = Physics.OverlapSphere(hitEnemySprite.position, chainRange);
        foreach(Collider chainedEnemyHurtbox in enemiesInProximity)
        {
            if(chainedEnemyHurtbox.gameObject.layer == 9 && chainedEnemyHurtbox.gameObject.tag != "Player")
            {
                print(chainedEnemyHurtbox.transform.parent.gameObject);
                if(chainedEnemyHurtbox.gameObject != hitEnemyHurtbox.gameObject)
                {
                    Transform chainedEnemySprite = chainedEnemyHurtbox.transform.parent.Find("Sprite").transform;

                    Vector3 vectorBetween = new Vector3(hitEnemySprite.position.x, hitEnemySprite.position.y, hitEnemySprite.position.z) - new Vector3(chainedEnemySprite.transform.position.x, chainedEnemySprite.transform.position.y, chainedEnemySprite.transform.position.z);
                    float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
                    Quaternion rotationEnToEn = Quaternion.Euler(transform.rotation.x, rotation + 90, transform.rotation.z);

                    GameObject newChain = Instantiate(lightningChain, new Vector3((hitEnemySprite.position.x + chainedEnemySprite.transform.position.x) / 2, (hitEnemySprite.position.y + chainedEnemySprite.transform.position.y) / 2, (hitEnemySprite.position.z + chainedEnemySprite.transform.position.z) / 2), rotationEnToEn);
                    newChain.transform.localScale = new Vector3(newChain.transform.localScale.x, newChain.transform.localScale.y, vectorBetween.magnitude);
                    ElementDamageType edt = this.gameObject.GetComponent<ElementDamageType>();
                    newChain.GetComponent<ElementDamageType>().SetDamageType(edt.damageType, edt.newMat);

                    chainedEnemyHurtbox.GetComponent<Damageable>().TakeDamage(1f);

                    if (!chainedEnemyHurtbox.TryGetComponent<LightningStun>(out LightningStun ls))
                    {
                        chainedEnemyHurtbox.AddComponent<LightningStun>();
                    }
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.tag != "Player")
        {
            ChainLightningEffect(other.transform, other.transform.parent.Find("Sprite").transform);
        }
    }
}
