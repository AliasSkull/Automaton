using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlip : MonoBehaviour
{
    public List<Goblin> gobbies;
    public PlayerController _pc;

    public float playerAccel;
    public float enemyDeccel;

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

        if (_pc != null)
        {
            _pc.accelerationRate = 10;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<Goblin>().gobbySpeed = enemyDeccel;
            gobbies.Add(other.gameObject.GetComponent<Goblin>());
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
            if (other.gameObject.GetComponent<Goblin>().gobbySpeed < 5f)
            {
                other.gameObject.GetComponent<Goblin>().gobbySpeed = 5f;
                gobbies.Remove(other.gameObject.GetComponent<Goblin>());
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
