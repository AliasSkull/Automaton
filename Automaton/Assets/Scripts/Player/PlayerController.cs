using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject Hitbox;
    private Collider hitbox;


    [Header("Movement Settings")]

    public float accelerationRate;
    public float deaccelerationRate;
    public float jumpForce;
    public float rotateSpeed;
    public float dashSpeed;
    public float currentDashTime;
    public float dashCoolDownTime;

    [Header("Sprite Settings")]
    public SpriteRenderer sprite;
    public Sprite Forward;
    public Sprite Right;
    public Sprite Left;
    public Sprite Back;

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

    [Header("Player Stats")]
    public float maxHealth;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
       
        _rb = GetComponent<Rigidbody>();
        hitbox = Hitbox.transform.GetComponent<Collider>();
        currentHealth = maxHealth;
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

        if (moveDir != Vector3.zero)
        {
            this.transform.forward = -moveDir;
        }

        if (moveDir == Vector3.back)
        {
            sprite.sprite = Forward;
        }
        else if (moveDir == Vector3.right)
        {
            sprite.sprite = Right;
            sprite.flipX = false;
        }
        else if (moveDir == Vector3.left)
        {
            sprite.sprite = Right;
            sprite.flipX = true;



        }
        else if (moveDir == Vector3.forward)
        {
            sprite.sprite = Back;
        }

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

        if (isAttacking)
        {
            hitbox.enabled = true;

        }
        else
        {
            hitbox.enabled = false;
        }



    }

    private void FixedUpdate()
    {
        
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
       

   
    }

    void ResetAttack() 
    {
        isAttacking = false;
        readyAttack = true;
        
    }


    public void AttackRayCast() 
    {
        //Sends raycast to detect enemy
        Debug.Log("ATTACK");
       
      
        if (Physics.Raycast(this.transform.position, moveDir, out RaycastHit hit, attackDistance, damageLayer))
         {

             //if raycasts hits then get script and activate takedamage function
             print(hit.collider.gameObject.name);
             if (hit.transform.TryGetComponent<Enemy>(out Enemy T))
             { T.TakeDamage(attackDamage); }
         }

    }


    public void Dash() 
    {
        _rb.AddForce(moveDir * dashSpeed * Time.deltaTime, ForceMode.Impulse);
        canDash = false;
        currentDashTime = dashCoolDownTime;
    }

    void HitTarget(Vector3 pos)
    {
        
    }

    public void TakeDamage(float damage) 
    {
        currentHealth = currentHealth - damage;
        Debug.Log("Player owwie");
    }


}
