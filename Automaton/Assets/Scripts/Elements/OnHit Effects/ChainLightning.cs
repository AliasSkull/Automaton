using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float chainRange;
    public float damage;
    public GameObject lightningVisual;
    public GameObject impactPoint;
    public GameObject missfire;
    private Vector3 impactStay;

    public RaycastHit hit;
    public Ray ray;
    public LayerMask layerMask ;
    public bool hasHit;
    
    // Start is called before the first frame update
    void Start()
    {
        lightningVisual.SetActive(false);
        missfire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHit)
        {
            ShootLightning();
        }
        else
        {
            impactPoint.transform.position = impactStay;
        }
    }

    public void ShootLightning()
    {
        ray = new Ray(new Vector3(this.gameObject.transform.position.x, 1.8f, this.gameObject.transform.position.z), -transform.forward);

        Debug.DrawRay(this.gameObject.transform.position + new Vector3(0, 1.8f, 0), Vector3.forward * 100, Color.red);

        if (Physics.Raycast(ray, out hit, 20, layerMask))
        {
            lightningVisual.SetActive(true);

            if (hit.collider.gameObject.layer == 9)
            {
                GameObject enemySprite = hit.collider.transform.parent.Find("Sprite").gameObject;

                impactStay = new Vector3(enemySprite.transform.position.x, enemySprite.transform.position.y, enemySprite.transform.position.z);

                ChainLightningEffect(hit.collider.transform, hit.collider.transform.parent.Find("Sprite").transform);
                hit.collider.gameObject.GetComponent<Damageable>().TakeDamage(10f, "");
                hasHit = true;
            }
            else if(hit.collider.gameObject.layer == 16)
            {
                impactStay = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);

                hit.collider.gameObject.GetComponent<LightningGen>().Esploud();

                hasHit = true;
            }
        }
        else
        {
            missfire.SetActive(true);
        }
    }

    public void ChainLightningEffect(Transform hitEnemyHurtbox, Transform hitEnemySprite)
    {
        Collider[] enemiesInProximity = Physics.OverlapSphere(hitEnemySprite.position, chainRange);
        foreach(Collider chainedEnemyHurtbox in enemiesInProximity)
        {
            if(chainedEnemyHurtbox.gameObject.layer == 9 && chainedEnemyHurtbox.transform.parent.tag != "Player")
            {
                if(chainedEnemyHurtbox.gameObject != hitEnemyHurtbox.gameObject)
                {
                    Transform chainedEnemySprite = chainedEnemyHurtbox.transform.parent.Find("Sprite").transform;

                    Vector3 vectorBetween = new Vector3(hitEnemySprite.position.x, hitEnemySprite.position.y, hitEnemySprite.position.z) - new Vector3(chainedEnemySprite.transform.position.x, chainedEnemySprite.transform.position.y, chainedEnemySprite.transform.position.z);
                    float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
                    Quaternion rotationEnToEn = Quaternion.Euler(transform.rotation.x, rotation + 90, transform.rotation.z);

                    GameObject newChain = GameObject.Find("LCPool(Clone)").transform.GetChild(0).gameObject;
                    GameObject impactPoint = newChain.transform.transform.GetChild(0).transform.Find("ImpactPoint").gameObject;
                    newChain.transform.SetParent(null);
                    newChain.transform.position = new Vector3((hitEnemySprite.position.x + chainedEnemySprite.transform.position.x) / 2, (hitEnemySprite.position.y + chainedEnemySprite.transform.position.y) / 2, (hitEnemySprite.position.z + chainedEnemySprite.transform.position.z) / 2);
                    newChain.transform.rotation = rotationEnToEn;
                    impactPoint.transform.position = new Vector3(chainedEnemySprite.transform.position.x, chainedEnemySprite.transform.position.y, chainedEnemySprite.transform.position.z);

                    chainedEnemyHurtbox.GetComponent<Damageable>().TakeDamage(damage, "");
                }

            }

        }
    }
}
