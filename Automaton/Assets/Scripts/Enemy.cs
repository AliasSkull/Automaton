using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float attackSpeed;
    Rigidbody rb;
    public Vector3 moveDir;

    public GameObject target;

    public bool isPlayerinRange;
    public bool isAttacking = false;
    public bool canAttack = true;
    private GameObject player;

    public GameObject Hitbox;
    public Collider hitbox;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isPlayerinRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(this.transform.position.x, 0, this.transform.position.z); ;
        moveDir.Normalize();

        if (moveDir != Vector3.zero)
        {
            this.transform.forward = moveDir;
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            isAttacking = true;
        }

        target.TryGetComponent<PlayerController>(out PlayerController P);
        P.TakeDamage(2);

        Invoke(nameof(ResetAttack), attackSpeed);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;

        Debug.Log("Ow");

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
       // Destroy(this.gameObject);
    }

    public void ResetAttack() 
    {

        isAttacking = false;
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("InRange");
        if (other.gameObject.tag == "Player" )
        {
            isPlayerinRange = true;
            target = other.gameObject;

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerinRange = false;
            target = null;
        }
    }

   
}
