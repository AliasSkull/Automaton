using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DamageOverTime", 0.1f, 0.7f);
        StartCoroutine(TimedDestruction());
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this);
    }

    public void DamageOverTime()
    {
        this.gameObject.GetComponent<Damageable>().TakeDamage(1);
    }
}
