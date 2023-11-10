using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float chainRange;
    public GameObject lightningChain;
    public GameObject lightningVisual;

    public RaycastHit hit;
    public Ray ray;
    public LayerMask layerMask ;
    public bool hasHit;
    
    // Start is called before the first frame update
    void Start()
    {
        lightningVisual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHit)
        {
            ShootLightning();
        }
        

    }

    public void ShootLightning()
    {
        ray = new Ray(new Vector3(this.gameObject.transform.position.x, 1.8f, this.gameObject.transform.position.z), -transform.forward);

        Debug.DrawRay(this.gameObject.transform.position + new Vector3(0, 1.8f, 0), Vector3.forward * 100, Color.red);

        if (Physics.Raycast(ray, out hit, 50, layerMask))
        {
            print(hit.collider.gameObject);
            lightningVisual.SetActive(true);

            GameObject enemySprite = hit.collider.transform.parent.Find("Sprite").gameObject;

            Vector3 midpoint = new Vector3((transform.position.x + enemySprite.transform.position.x) / 2, 1.8f, (transform.position.z + enemySprite.transform.position.z) / 2);

            float length = Vector3.Distance(enemySprite.transform.position, transform.position);

            lightningVisual.transform.position = midpoint;
            lightningVisual.transform.localScale = new Vector3(lightningVisual.transform.localScale.x, lightningVisual.transform.localScale.y, length);

            ChainLightningEffect(hit.collider.transform, hit.collider.transform.parent.Find("Sprite").transform);
            hasHit = true;
        }
        

    }

    public void ChainLightningEffect(Transform hitEnemyHurtbox, Transform hitEnemySprite)
    {
        Collider[] enemiesInProximity = Physics.OverlapSphere(hitEnemySprite.position, chainRange);
        foreach(Collider chainedEnemyHurtbox in enemiesInProximity)
        {
            if(chainedEnemyHurtbox.gameObject.layer == 9 && chainedEnemyHurtbox.transform.parent.tag != "Player")
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

                    chainedEnemyHurtbox.GetComponent<Damageable>().TakeDamage(1f, "");

                    switch (edt.damageType)
                    {
                        case 0:
                            if (chainedEnemyHurtbox.TryGetComponent<FireDot>(out FireDot fDOT))
                            {
                                Destroy(fDOT);
                                chainedEnemyHurtbox.AddComponent<FireDot>();
                            }
                            else
                            {
                                chainedEnemyHurtbox.AddComponent<FireDot>();
                            }

                            break;
                        case 1:
                            if (!chainedEnemyHurtbox.TryGetComponent<WaterPushback>(out WaterPushback wpb))
                            {
                                chainedEnemyHurtbox.AddComponent<WaterPushback>();
                            }
                            break;
                        case 2:

                            break;
                    }
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.transform.parent.tag != "Player")
        {
            ChainLightningEffect(other.transform, other.transform.parent.Find("Sprite").transform);
        }
    }
}
