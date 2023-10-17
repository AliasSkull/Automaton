using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    public float attackSpeed;
    public Vector3 objectDirection;
    public float damageCount;
    public Damageable damageScript;
    
    

    public Collider hitbox;
    public Rigidbody rb;

    private bool readyAttack;
    private bool isAttacking;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        damageScript = GetComponentInChildren<Damageable>();
        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        damageCount = GetComponentInChildren<Damageable>().damageCount;

        if (damageScript.currentHealth <= 0)
        {
            Death();
        }
    }

    public void Attack() 
    {
        if (!readyAttack || isAttacking) return;

        readyAttack = false;
        isAttacking = true;

        hitbox.enabled = true;
        rb.AddForce(Vector3.forward, ForceMode.Impulse);
        Invoke(nameof(ResetAttack), attackSpeed);



    }

    public void ResetAttack() 
    {
        isAttacking = false;
        readyAttack = true;
        hitbox.enabled = false;

    }


    public void Death() 
    {

        Destroy(this.gameObject);
    }
}
