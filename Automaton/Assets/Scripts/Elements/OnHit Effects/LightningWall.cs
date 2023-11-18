using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWall : MonoBehaviour
{

    public List<GameObject> goblinsInLightning;

    private bool canHurt;
    
    // Start is called before the first frame update
    void Start()
    {
        canHurt = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(goblinsInLightning.Count > 0 && canHurt)
        {
            for (int i = 0; i < goblinsInLightning.Count; i++)
            {
                goblinsInLightning[i].GetComponent<Damageable>().TakeDamage(3, "");
            }
            Invoke("ResetAttack", 0.7f);
            canHurt = false;
        }
    }

    public void ResetAttack()
    {
        canHurt = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 && other.transform.parent.gameObject.layer == 7)
        {
            goblinsInLightning.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && other.transform.parent.gameObject.layer == 7)
        {
            for(int i = 0; i < goblinsInLightning.Count; i++)
            {
                if (other.gameObject == goblinsInLightning[i])
                {
                    goblinsInLightning.RemoveAt(i);
                }
            }
        }
    }
}
