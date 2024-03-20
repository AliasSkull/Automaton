using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLightnBlast : MonoBehaviour
{
    private float timer;
    public float timeTillDeletion;
    public GameObject secondBlastPrefab;
    private float lerpValue;

    private float startSize;
    private float endSize;
    private int extraDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        float variance = 0;
        if (pa.element1.name == "Lightning Wave")
        {
            variance = StaticValues.lSizeBuildup;
            extraDamage = (int)StaticValues.lDamageBuildup;

        }
        else if (pa.element2.name == "Lightning Wave")
        {
            variance = StaticValues.rSizeBuildup;
            extraDamage = (int)StaticValues.rDamageBuildup;
        }

        startSize = 1 + variance;
        endSize = 5 + variance;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < timeTillDeletion)
        {
            lerpValue = Mathf.Lerp(startSize, endSize, timer / timeTillDeletion);
            this.transform.localScale = new Vector3(lerpValue, transform.position.y, lerpValue);
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<Damageable>().TakeDamage(15 + extraDamage, 9);
        }

        if(other.gameObject.layer == 16)
        {
            if(secondBlastPrefab != null)
            {
                GameObject secBlast = Instantiate(secondBlastPrefab, other.transform.position, other.transform.rotation);
                secBlast.transform.SetParent(null);
                other.gameObject.GetComponent<LightningGen>().Esploud();
            }
        }
    }

}
