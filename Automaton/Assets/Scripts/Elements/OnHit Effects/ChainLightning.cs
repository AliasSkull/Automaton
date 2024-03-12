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
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHit)
        {
        }
        else
        {
            impactPoint.transform.position = impactStay;
            missfire.SetActive(true);
        }
    }

    public void ChainLightningEffect(Transform hitEnemyHurtbox, Transform hitEnemySprite, bool generator)
    {
        float range = chainRange;

        if (generator)
        {
            range = 10;
        }
        
        Collider[] enemiesInProximity = Physics.OverlapSphere(hitEnemySprite.position, range);
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
                    newChain.transform.position = hitEnemySprite.position;
                    newChain.transform.rotation = rotationEnToEn;
                    impactPoint.transform.position = new Vector3(chainedEnemySprite.transform.position.x, chainedEnemySprite.transform.position.y, chainedEnemySprite.transform.position.z);

                    chainedEnemyHurtbox.GetComponent<Damageable>().TakeDamage(damage, 2);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject);
        
        if (other.gameObject.layer == 9 && !hasHit)
        {
            print(other.gameObject);
            GameObject enemySprite = other.gameObject.transform.parent.Find("Sprite").gameObject;

            impactStay = new Vector3(enemySprite.transform.position.x, enemySprite.transform.position.y, enemySprite.transform.position.z);

            ChainLightningEffect(other.gameObject.transform, other.gameObject.transform.parent.Find("Sprite").transform, false);
            other.gameObject.GetComponent<Damageable>().TakeDamage(10f, 2);
            hasHit = true;
        }
        
        if (other.gameObject.layer == 16 && !hasHit)
        {
            print(other.gameObject);

            impactStay = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);

            other.gameObject.gameObject.GetComponent<LightningGen>().Esploud();
            ChainLightningEffect(other.gameObject.transform, other.gameObject.transform, true);
            hasHit = true;
        }
    }
}
