using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainWind : MonoBehaviour
{
    public List<Goblin> gobbies;
    public PlayerController _pc;

    private void OnDestroy()
    {
        if (_pc != null)
        {
            _pc.accelerationRate = 10;
        }
    }

    public void UnSpeed()
    {
        foreach(Goblin gob in gobbies)
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
            other.gameObject.GetComponent<Goblin>().gobbySpeed = 2.5f;
            gobbies.Add(other.gameObject.GetComponent<Goblin>());
        }

        if(other.gameObject.tag == "Player")
        {
            _pc = other.gameObject.GetComponent<PlayerController>();
            _pc.accelerationRate = 13f;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            if(other.gameObject.GetComponent<Goblin>().gobbySpeed == 2.5f)
            {
                other.gameObject.GetComponent<Goblin>().gobbySpeed = 5f;
                gobbies.Remove(other.gameObject.GetComponent<Goblin>());
            }
        }

        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerController>().accelerationRate == 13f)
            {
                other.gameObject.GetComponent<PlayerController>().accelerationRate = 10f;
            }
        }
    }
}
