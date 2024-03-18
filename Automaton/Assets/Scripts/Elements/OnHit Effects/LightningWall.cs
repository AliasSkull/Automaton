using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWall : MonoBehaviour
{
    public List<GameObject> goblinsInLightning;

    private bool canHurt;
    private int extraDamage;

    public LightningGen[] gen = new LightningGen[0];
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerAimer pa = GameObject.Find("PlayerAimer").GetComponent<PlayerAimer>();

        if (pa.element1.name == "Lightning Storm")
        {
            extraDamage = (int)StaticValues.lDamageBuildup;

        }
        else if (pa.element2.name == "Lightning Storm")
        {
            extraDamage = (int)StaticValues.rDamageBuildup;
        }

        canHurt = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(goblinsInLightning.Count > 0 && canHurt)
        {
            for (int i = 0; i < goblinsInLightning.Count; i++)
            {
                if(goblinsInLightning[i] != null)
                {
                    Vector3 vel = goblinsInLightning[i].transform.parent.gameObject.GetComponent<Rigidbody>().velocity;

                    int damageTaken = 3;
                    if (vel.magnitude > 10)
                    {
                        damageTaken = 7;
                    }

                    goblinsInLightning[i].GetComponent<Damageable>().TakeDamage(damageTaken + extraDamage, 4); 
                }
                else
                {
                    goblinsInLightning.RemoveAt(i);
                }
            }

            if(gen.Length != 0)
            {
                foreach (LightningGen script in gen)
                {
                    script.Esploud();
                }
            }


            Invoke("ResetAttack", 0.7f);
            canHurt = false;
        }

        if(gen.Length != 0 && canHurt && goblinsInLightning.Count <= 0)
        {
            foreach (LightningGen script in gen)
            {
                Invoke("ResetAttack", 0.7f);
                canHurt = false;
                
                script.Esploud();
            }
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

        if(other.gameObject.layer == 16)
        {
            gen = other.gameObject.GetComponents<LightningGen>();
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
