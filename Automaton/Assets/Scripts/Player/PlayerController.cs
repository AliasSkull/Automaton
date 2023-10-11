using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject Hitbox;
    private Collider hitbox;
    public Camera cam;

    [Header("Movement Settings")]

    public float accelerationRate;
    public float deaccelerationRate;
    public float rotationRate;
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

    public Vector3 moveDir;

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

    [Header("Animation")]
    public Animator player;
    public SpriteRenderer sprite;

    [Header("Sound Effects")]

    public AudioSource sound;
    public AudioClip meleeAttack;

    [Header("Cursor")]

    public Texture2D cursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;



    // Start is called before the first frame update
    void Start()
    {
       
        _rb = GetComponent<Rigidbody>();
        hitbox = Hitbox.transform.GetComponent<Collider>();
        currentHealth = maxHealth;
        Cursor.visible = true;
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {


        if (!isAttacking)
        {
            Movement();
        }
        //LookatMouse();
        RotateSprite();

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


        AnimationHandler();

    }

    private void FixedUpdate()
    {
        
    }


    public void Movement() 
    {
        currentVelocity = _rb.velocity;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);
        moveDir.Normalize();

        _rb.velocity = moveDir * accelerationRate;

    
    }



    public void RotateSprite() 
    {
        var playerScreenPoint = Camera.main.WorldToScreenPoint(this.transform.position);

    }

    public void AnimationHandler()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            player.SetBool("isRunning", true);

        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
        {
            player.SetBool("isRunning", false);

        }

        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        if (mouse.x > playerScreenPoint.x + 20f)
        {
            //left
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = false;
        }
        else if (mouse.x < playerScreenPoint.x - 20f)
        {
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = true;
        }
        else if (mouse.y > playerScreenPoint.y)
        {
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", true);
        }
        else
        {
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", false);

        }

        if (Input.GetMouseButton(0))
        {
            player.SetBool("isRunning", false);
            player.SetTrigger("Attacking");




        }
    }



    public void MeleeAttack() 
    {
        if (!readyAttack || isAttacking) return;

        readyAttack = false;
        isAttacking = true;

        sound.PlayOneShot(meleeAttack);
        Invoke(nameof(AttackRayCast), attackSpeed);
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
       
       
      
        if (Physics.Raycast(this.transform.position, moveDir, out RaycastHit hit, attackDistance, damageLayer))
         {

             //if raycasts hits then get script and activate takedamage function
             print(hit.collider.gameObject.name);
             if (hit.transform.TryGetComponent<Dummy>(out Dummy T))
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
        //Debug.Log("Player owwie");
    }


}
