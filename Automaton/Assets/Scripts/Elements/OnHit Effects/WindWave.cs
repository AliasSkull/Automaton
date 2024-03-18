using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWave : MonoBehaviour
{
    public GameObject secondBlast;
    private GameObject player;

    private int mouseButton;
    private int extraDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.SetParent(player.transform);

        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        float variance = 0;
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

        transform.position = transform.parent.position;
    }

    private void OnDestroy()
    {
        GameObject secBlast = Instantiate(secondBlast, this.transform.position, this.transform.rotation);

        secBlast.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(2, 0, this.transform.position, true);
                gob.damageScript.TakeDamage(3 + extraDamage, 3);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(2, 0, this.transform.position, true);
                rGob.damageScript.TakeDamage(3 + extraDamage, 3);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
            {
                srGob.StartCrowdControl(2, 0, this.transform.position, true);
                srGob.damageScript.TakeDamage(3 + extraDamage, 3);
            }
        }
    }
}
