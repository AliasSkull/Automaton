using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Movement Settings")]

    public float accelerationRate;
    public float deaccelerationRate;
    public float jumpForce;
    public float rotateSpeed;
    public float dashSpeed;
    public float currentDashTime;
    public float dashCoolDownTime;
  


    [Header("Ground Layer")]
    public LayerMask groundMask;

    [Header("Movement Checks")]
    public bool isMoving;
    public bool canDash;
    

    public Vector3 currentVelocity;

    private Vector3 moveDir;

    [Header("Melee Attack")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public float attackDamage = 1;
    public float attackCount = 0;
    public LayerMask damageLayer;

    public bool isAttacking = false;
    public bool readyAttack = true;

    // Start is called before the first frame update
    void Start()
    {
       
        _rb = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {

      
        currentVelocity = _rb.velocity;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);
        moveDir.Normalize();
       
        _rb.velocity = moveDir * accelerationRate;

       
        if (Input.GetKeyUp(KeyCode.Space) && canDash)
        {
            
            Dash();
           
        }

        if (Input.GetMouseButton(0))
        {
            MeleeAttack();
        }


        if (currentDashTime > 0)
        {
            currentDashTime -= Time.deltaTime;
        }
        else if (currentDashTime <= 0)
        {
            canDash = true;
        }

       

    }

  
    public void Movement() 
    {

    
    }

    public void MeleeAttack() 
    {
        if (!readyAttack || isAttacking) return;

        readyAttack = false;
        isAttacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRayCast), attackDelay);


        
    }

    void ResetAttack() 
    {
        isAttacking = false;
        readyAttack = true;
        
    }


    public void AttackRayCast() 
    {
        Debug.Log("ATTACK");
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, -moveDir, out hit, attackDistance, damageLayer))
        {
            print(hit.collider.gameObject.name);
            if (hit.transform.TryGetComponent<Enemy>(out Enemy T))
            { T.TakeDamage(attackDamage); }
        }
    }


    public void Dash() 
    {
        _rb.AddForce(moveDir * dashSpeed, ForceMode.Impulse);
        canDash = false;
        currentDashTime = dashCoolDownTime;
    }

    void HitTarget(Vector3 pos)
    {
        
    }




}
