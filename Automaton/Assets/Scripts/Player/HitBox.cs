using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public PlayerController player;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable")
        {
            if (other.transform.TryGetComponent<Dummy>(out Dummy T))
            { T.TakeDamage(player.attackDamage); }
            StartCoroutine("AttackWait");
            StopCoroutine("AttackWait");
        }
    }

    IEnumerator AttackWait() 
    {

        yield return new WaitForSeconds(2);
       

    }


}
