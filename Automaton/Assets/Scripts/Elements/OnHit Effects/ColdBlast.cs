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

    // Start is called before the first frame update
    void Start()
    {
        freezable = true;
        _rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, 0, -10000);

        _rb.AddRelativeForce(force * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        if (!seconded)
        {
            SecondBlast();
        }
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
        secondObject.SetActive(true);
        secondObject.GetComponent<TimedDestruction>().enabled = true;

        secondObject.transform.SetParent(null);

        foreach(Transform trans in secondObject.transform)
        {
            Vector3 force = new Vector3(0, 2000, 0);
            Rigidbody currentRB = trans.gameObject.GetComponent<Rigidbody>();
            currentRB.AddRelativeForce(force * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && freezable)
        {
            Goblin gob = other.gameObject.GetComponent<Goblin>();
            gob.StartCrowdControl(1, 4, this.transform.position, false);
            gob.damageScript.TakeDamage(5, " Freeze");

            freezable = false;

            SecondBlast();
            seconded = true;
        }
    }
}
