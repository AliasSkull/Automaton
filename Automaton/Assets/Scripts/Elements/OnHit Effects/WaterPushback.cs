using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPushback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.gameObject.GetComponent<Goblin>().Pushback();
        //GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot").position, "Push", 1);
        StartCoroutine(TimedDestruction());
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(0.03f);
        transform.parent.gameObject.GetComponent<Goblin>().Pushback();
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
