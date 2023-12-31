using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainIce : MonoBehaviour
{
    public float chainRange;
    public GameObject lightningVisual;

    public RaycastHit hit;
    public Ray ray;
    public LayerMask layerMask;
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

        if (Physics.Raycast(ray, out hit, 20, layerMask))
        {
            lightningVisual.SetActive(true);

            GameObject enemySprite = hit.collider.transform.parent.Find("Sprite").gameObject;

            Vector3 midpoint = new Vector3((transform.position.x + enemySprite.transform.position.x) / 2, ((transform.position.y + enemySprite.transform.position.y) / 2) - 0.4f, (transform.position.z + enemySprite.transform.position.z) / 2);

            float length = Vector3.Distance(enemySprite.transform.position, transform.position);

            lightningVisual.transform.position = midpoint;
            lightningVisual.transform.localScale = new Vector3(lightningVisual.transform.localScale.x, lightningVisual.transform.localScale.y, length);

            ChainLightningEffect(hit.collider.transform, hit.collider.transform.parent.Find("Sprite").transform);
            hit.collider.gameObject.GetComponent<Damageable>().TakeDamage(10f, "");

            Transform newWall =GameObject.Find("CIPool(Clone)").transform.Find("Wall").GetChild(0);
            newWall.SetParent(null);
            newWall.position = lightningVisual.transform.position;
            newWall.localScale = new Vector3(lightningVisual.transform.localScale.z, newWall.localScale.y, newWall.localScale.z);
            newWall.eulerAngles = new Vector3(lightningVisual.transform.rotation.eulerAngles.x, lightningVisual.transform.rotation.eulerAngles.y + 90, lightningVisual.transform.rotation.eulerAngles.z);

            hasHit = true;
        }

    }

    public void ChainLightningEffect(Transform hitEnemyHurtbox, Transform hitEnemySprite)
    {
        Collider[] enemiesInProximity = Physics.OverlapSphere(hitEnemySprite.position, chainRange);
        foreach (Collider chainedEnemyHurtbox in enemiesInProximity)
        {
            if (chainedEnemyHurtbox.gameObject.layer == 9 && chainedEnemyHurtbox.transform.parent.tag != "Player")
            {
                if (chainedEnemyHurtbox.gameObject != hitEnemyHurtbox.gameObject)
                {
                    Transform chainedEnemySprite = chainedEnemyHurtbox.transform.parent.Find("Sprite").transform;

                    Vector3 vectorBetween = new Vector3(hitEnemySprite.position.x, hitEnemySprite.position.y, hitEnemySprite.position.z) - new Vector3(chainedEnemySprite.transform.position.x, chainedEnemySprite.transform.position.y, chainedEnemySprite.transform.position.z);
                    float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
                    Quaternion rotationEnToEn = Quaternion.Euler(transform.rotation.x, rotation + 90, transform.rotation.z);

                    GameObject newChain = GameObject.Find("CIPool(Clone)").transform.Find("Chain").transform.GetChild(0).gameObject;
                    newChain.transform.SetParent(null);
                    newChain.transform.position = new Vector3((hitEnemySprite.position.x + chainedEnemySprite.transform.position.x) / 2, ((hitEnemySprite.position.y + chainedEnemySprite.transform.position.y) / 2) - 0.2f, (hitEnemySprite.position.z + chainedEnemySprite.transform.position.z) / 2);
                    newChain.transform.rotation = rotationEnToEn;
                    newChain.transform.localScale = new Vector3(newChain.transform.localScale.x, newChain.transform.localScale.y, vectorBetween.magnitude);

                    chainedEnemyHurtbox.GetComponent<Damageable>().TakeDamage(10f, "");
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
