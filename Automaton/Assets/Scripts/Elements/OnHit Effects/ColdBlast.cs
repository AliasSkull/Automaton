using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBlast : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    private bool freezable;
    private Rigidbody _rb;

    private bool seconded;
    private int extraDamage;

    // Start is called before the first frame update
    void Start()
    {
        freezable = true;
        _rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, 0, -10000);

        _rb.AddRelativeForce(force * Time.deltaTime, ForceMode.Impulse);

        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Ice Blast")
        {
            extraDamage = (int)StaticValues.lDamageBuildup;

        }
        else if (pa.element2.name == "Ice Blast")
        {
            extraDamage = (int)StaticValues.rDamageBuildup;
        }
    }

    private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SecondBlast()
    {
        _rb.isKinematic = true;
        _rb.velocity = new Vector3(0, 0, 0);
        firstObject.SetActive(false);
        GameObject go = Instantiate(secondObject, new Vector3(this.transform.position.x, secondObject.transform.position.y, this.transform.position.z), secondObject.transform.rotation);

        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Ice Blast")
        {
            go.transform.localScale = new Vector3(go.transform.localScale.x + StaticValues.lSizeBuildup, go.transform.localScale.y, go.transform.localScale.z + StaticValues.lSizeBuildup);
        }
        else if (pa.element2.name == "Ice Blast")
        {
            go.transform.localScale = new Vector3(go.transform.localScale.x + StaticValues.rSizeBuildup, go.transform.localScale.y, go.transform.localScale.z + StaticValues.rSizeBuildup);
        }


        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && freezable)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(1, 4, this.transform.position, false);
                gob.damageScript.TakeDamage(10 + extraDamage, 6);
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(1, 4, this.transform.position, false);
                rGob.damageScript.TakeDamage(10 + extraDamage, 6);
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin rsGob))
            {
                rsGob.StartCrowdControl(1, 4, this.transform.position, false);
                rsGob.damageScript.TakeDamage(10 + extraDamage, 6);
            }

            if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && other.gameObject.name == "Dummy(Clone)")
            {
                other.gameObject.transform.Find("Hurtbox").gameObject.GetComponent<Damageable>().TakeDamage(10, 1);
            }

            freezable = false;

            SecondBlast();
            seconded = true;
        }
    }
}
