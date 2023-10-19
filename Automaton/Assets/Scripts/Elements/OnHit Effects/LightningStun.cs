using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.gameObject.GetComponent<Goblin>().Stun();
        //GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot").position, "Stun", 1);
        StartCoroutine(TimedDestruction());
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(0.05f);
        transform.parent.gameObject.GetComponent<Goblin>().Stun();
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
