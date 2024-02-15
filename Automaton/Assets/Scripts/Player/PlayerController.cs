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
    public bool isDashing;
    public bool canDash;
    public Vector3 currentVelocity;
    public Vector3 moveDir;
    public Image dashCooldown;
    private float timer;

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
    public HeartUIManagement heartScript;
    private bool damageable;

    [Header("Animation")]
    public Animator player;
    public SpriteRenderer sprite;
    private Color ogColor;

    [Header("Sound Effects")]

    public AudioSource sound;
    public AudioClip meleeAttack;

    [Header("Cursor")]

    public Texture2D cursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("UI")]
    public Slider healthSlide;



    // Start is called before the first frame update
    void Start()
    {
        Hitbox.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        Cursor.visible = false;
        //Cursor.SetCursor(cursor, hotSpot, cursorMode);
        ogColor = sprite.color;
        damageable = true;
        canDash = true;

        dashCooldown.type = Image.Type.Filled;
        dashCooldown.fillAmount = 0;


       
    }

    // Update is called once per frame
    void Update()
    {

        playerRotation = playerAimer.rotationPlayerToCursor;

        if (!isAttacking && !isDashing)
        {
            Movement();
        }

        if (Input.GetButtonDown("Dash") && !isDashing && canDash)
        {
            Dash();
            isDashing = true;
            canDash = false;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            MeleeAttack();
        }

      

        AnimationHandler();

    }

    private void FixedUpdate()
    {
        
    }


    public void Movement() 
    {
        currentVelocity = _rb.velocity;

        float x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJ");
        float z = Input.GetAxis("Vertical") + Input.GetAxis("VerticalJ");

        moveDir = new Vector3(x, transform.position.y, z);
        moveDir.Normalize();
        
        _rb.velocity = moveDir * accelerationRate;
    }


    public void AnimationHandler()
    {
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            player.SetBool("isRunning", true);

        }
        else
        {
            player.SetBool("isRunning", false);

        }


        if (playerAimer.transform.rotation.eulerAngles.y >= 34 && playerAimer.transform.rotation.eulerAngles.y <= 125)
        {
            //right
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = false;

        }
        else if (playerAimer.transform.rotation.eulerAngles.y >= 235 && playerAimer.transform.rotation.eulerAngles.y <= 324)
        {
            //left
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = true;
        }
        else if (playerAimer.transform.rotation.eulerAngles.y >= 124 && playerAimer.transform.rotation.eulerAngles.y <= 235)
        {
            //back
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", true);
        }
        else if (playerAimer.transform.rotation.eulerAngles.y <= 35 && playerAimer.transform.eulerAngles.y >= 0)
        {
            //front
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", false);

        }
        else if (playerAimer.transform.rotation.eulerAngles.y >= 325 && playerAimer.transform.eulerAngles.y <= 360)
        {
            
                //front
                player.SetBool("FacingLeft", false);
                player.SetBool("FacingBack", false);

        }
    }

    public void HealPlayer()
    {
        currentHealth = maxHealth;
        heartScript.HealPlayer();
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
        _rb.AddForce(moveDir * dashSpeed, ForceMode.Impulse);
        Invoke("StopDash", 0.2f);
        Invoke("DashCooldown", 1f);
        StartCoroutine(DashCooldownUI(1f));
    }

    public void StopDash()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        isDashing = false;
    }

    public void DashCooldown()
    {
        canDash = true;
    }

    public IEnumerator DashCooldownUI(float cooldown)
    {
        dashCooldown.fillAmount = 1;

        while (timer <= cooldown)
        {
            timer += Time.deltaTime;

            if (dashCooldown != null)
            {
                dashCooldown.fillAmount = -((timer / cooldown) - 1);
            }

            yield return null;
        }

        timer = 0;
    }

    public void TakeDamage() 
    {
        if (damageable)
        {
            damageable = false;
            currentHealth = currentHealth - 1;
            if (currentHealth == 0)
            {
                GameObject.Find("POC Manager").GetComponent<POCmanager>().PlayerRespawn(this);
            }
            StartCoroutine(TakingDamageCooldown(0.15f));
            heartScript.TakeDamage();
        }
    }

    public IEnumerator TakingDamageCooldown(float frquency)
    {
        sprite.color = new Color(255, 0,0);
        yield return new WaitForSeconds(frquency);
        sprite.color = new Color(ogColor.r, ogColor.g, ogColor.b, 0.4f);
        yield return new WaitForSeconds(frquency);
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(frquency);
        sprite.color = new Color(ogColor.r, ogColor.g, ogColor.b, 0.4f);
        yield return new WaitForSeconds(frquency);
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(frquency);
        sprite.color = ogColor;

        damageable = true;
    }


}
