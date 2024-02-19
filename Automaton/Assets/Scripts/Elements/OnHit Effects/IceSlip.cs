using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlip : MonoBehaviour
{
    public List<Goblin> gobbies;
    public List<RangeGoblin> rgobbies;
    public List<Damageable> hurtboxes;
    public PlayerController _pc;

    public float playerAccel;
    public float enemyDeccel;

    private bool canHurt = true;

    private void OnDestroy()
    {
        UnSpeed();
    }

    public void UnSpeed()
    {
        foreach (Goblin gob in gobbies)
        {
            gob.gobbySpeed = 5f;
        }

        foreach (RangeGoblin rgob in rgobbies)
        {
            rgob.gobbySpeed = 5f;
        }

        if (_pc != null)
        {
            _pc.accelerationRate = 10;
        }
    }

    private void Update()
    {
        if (hurtboxes.Count > 0 && canHurt)
        {
            for (int i = 0; i < hurtboxes.Count; i++)
            {
                if (hurtboxes[i] != null)
                {
                    hurtboxes[i].TakeDamage(3, "");
                }
                else
                {
                    hurtboxes.RemoveAt(i);
                }
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
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.gameObject.GetComponent<Goblin>().gobbySpeed = enemyDeccel;
                gobbies.Add(gob.gameObject.GetComponent<Goblin>());
                hurtboxes.Add(gob.transform.Find("Hurtbox").gameObject.GetComponent<Damageable>());
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.gameObject.GetComponent<RangeGoblin>().gobbySpeed = enemyDeccel;
                rgobbies.Add(rGob.gameObject.GetComponent<RangeGoblin>());
                hurtboxes.Add(rGob.transform.Find("Hurtbox").gameObject.GetComponent<Damageable>());
            }
        }

        if (other.gameObject.tag == "Player")
        {
            _pc = other.gameObject.GetComponent<PlayerController>();
            _pc.accelerationRate = playerAccel;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.gameObject.GetComponent<Goblin>().gobbySpeed = 5f;
                gobbies.Remove(gob.gameObject.GetComponent<Goblin>());
                hurtboxes.Remove(gob.transform.Find("Hurtbox").gameObject.GetComponent<Damageable>());
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.gameObject.GetComponent<RangeGoblin>().gobbySpeed = 5f;
                rgobbies.Remove(rGob.gameObject.GetComponent<RangeGoblin>());
                hurtboxes.Remove(rGob.transform.Find("Hurtbox").gameObject.GetComponent<Damageable>());
            }
        }

        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().accelerationRate == playerAccel)
            {
                other.gameObject.GetComponent<PlayerController>().accelerationRate = 10f;
            }
        }
    }
}
