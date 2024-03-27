using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWave : MonoBehaviour
{
    private float timer;
    public float timeTillDeletion;
    private float lerpValue;

    private float startSize;
    private float endSize;

    private int extraDamage;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        float variance =  0;
        if (pa.element1.name == "Wind Wave")
        {
            variance = StaticValues.lSizeBuildup;
            extraDamage = (int)StaticValues.lDamageBuildup;
            
        }
        else if (pa.element2.name == "Wind Wave")
        {
            variance = StaticValues.rSizeBuildup;
            extraDamage = (int)StaticValues.rDamageBuildup;
        }

        startSize = 1 + variance;
        endSize = 2.5f + variance;
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
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(2, 0, this.transform.position, true);
                gob.damageScript.TakeDamage(5 + extraDamage, 3);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(2, 0, this.transform.position, true);
                rGob.damageScript.TakeDamage(5 + extraDamage, 3);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(2, 0, this.transform.position, true);
                srGob.damageScript.TakeDamage(5 + extraDamage, 3);
            }
        }

        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && other.gameObject.name == "Dummy(Clone)")
        {
            other.gameObject.GetComponent<MeleeDummy>().Push(this.transform.position, true);
        }
    }
}
