using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject Hitbox;
    public Camera cam;
    public PlayerAimer playerAimer;

    [Header("Movement Settings")]

    public float accelerationRate;
    public float deaccelerationRate;
    public float rotationRate;
    public float jumpForce;
    public float rotateSpeed;
    public float dashSpeed;
    public float currentDashTime;
    public float dashCoolDownTime;
    public Quaternion playerRotation;

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
    public Collider meleeRange;

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

    [Header("UI")]

    public Canvas UI;
    public Slider healthSlide;



    // Start is called before the first frame update
    void Start()
    {
        Hitbox.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        Cursor.visible = true;
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
       
   

       
    }

    // Update is called once per frame
    void Update()
    {
        healthSlide.maxValue = maxHealth;
        healthSlide.value = currentHealth;

        playerRotation = playerAimer.rotationPlayerToCursor;

        if (!isAttacking)
        {
            Movement();
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


        if (this.transform.rotation.eulerAngles.y >= 34 && this.transform.rotation.eulerAngles.y <= 125)
        {
            //right
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = true;

        }
        else if (this.transform.rotation.eulerAngles.y >= 235 && this.transform.rotation.eulerAngles.y <= 324)
        {
            //left
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = false;
        }
        else if (this.transform.rotation.eulerAngles.y >= 124 && this.transform.rotation.eulerAngles.y <= 235)
        {
            //back
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", true);
        }
        else if (this.transform.rotation.eulerAngles.y <= 35 && this.transform.eulerAngles.y >= 0)
        {
            //front
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", false);

        }
        else if (this.transform.rotation.eulerAngles.y >= 325 && this.transform.eulerAngles.y <= 360)
        {
            
                //front
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
        Hitbox.SetActive(true);
        sound.PlayOneShot(meleeAttack);
        Invoke(nameof(AttackRayCast), attackDelay);
        Invoke(nameof(ResetAttack), attackSpeed);
       

   
    }

    void ResetAttack() 
    {
        isAttacking = false;
        readyAttack = true;
        Hitbox.SetActive(false); ;
        
    }


    public void AttackRayCast() 
    {
        meleeRange.enabled = true;

    }


    public void Dash() 
    {
        _rb.AddForce(moveDir * dashSpeed * Time.deltaTime, ForceMode.Impulse);
        canDash = false;
        currentDashTime = dashCoolDownTime;
    }

  
    public void TakeDamage(float damage) 
    {
        currentHealth = currentHealth - damage;
        GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.Find("DamageTextSpot").position, damage.ToString(), 0);
        //print("Player has been hit by Goblin");
    }


}
