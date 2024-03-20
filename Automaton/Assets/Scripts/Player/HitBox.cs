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
     
        if (other.gameObject.layer == 9)
        {

            
            other.transform.TryGetComponent<Damageable>(out Damageable D);
                D.TakeDamage(player.attackDamage, 100);
          
           
        }
    }

    IEnumerator AttackWait() 
    {

        yield return new WaitForSeconds(2);
       

    }


}
