using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePillar : MonoBehaviour
{
    public List<GameObject> goblinsInFire;

    private bool canHurt;

    // Start is called before the first frame update
    void Start()
    {
        canHurt = true;
    }

    private void OnDisable()
    {
        if(goblinsInFire != null)
        {
            goblinsInFire.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (goblinsInFire.Count > 0 && canHurt)
        {
            for (int i = 0; i < goblinsInFire.Count; i++)
            {
                if (goblinsInFire[i] != null)
                {
                    goblinsInFire[i].GetComponent<Damageable>().TakeDamage(2, 11);
                }
                else
                {
                    goblinsInFire.RemoveAt(i);
                }
            }
            Invoke("ResetAttack", 0.4f);
            canHurt = false;
        }
    }

    public void ResetAttack()
    {
        canHurt = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.transform.parent.gameObject.layer == 7)
        {
            goblinsInFire.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && other.transform.parent.gameObject.layer == 7)
        {
            for (int i = 0; i < goblinsInFire.Count; i++)
            {
                if (other.gameObject == goblinsInFire[i])
                {
                    goblinsInFire.RemoveAt(i);
                }
            }
        }
    }
}
