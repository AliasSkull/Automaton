using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RangeGoblin : MonoBehaviour
{
    public float attackSpeed;
    public Vector3 objectDirection;
    public float damageCount;
    public float stunTime;
    public float gobbySpeed;
    public Damageable damageScript;

    public Canvas goblinUI;
    public Image exlaimationP;

    public Collider hitbox;
    public Rigidbody rb;

    private bool readyAttack;
    private bool isAttacking;
    private bool sliding;

    public GameObject player;
    public bool stunned;
    public bool pushedBack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.gameObject;

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
        readyAttack = false;
        isAttacking = true;



    }

    void ResetAttack()
    {
        isAttacking = false;
        readyAttack = true;
      

    }

    public void ShowExclaimation()
    {
        exlaimationP.enabled = true;

    }

    public void Death() 
    { 
    
    
    }

}
