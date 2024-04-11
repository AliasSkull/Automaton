using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;



public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject Hitbox;
    public Camera cam;
    public PlayerAimer playerAimer;
    public LevelManager _lm;
    public TutorialManager tm;
    public DialogueManager dm;

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
    private Controller input = null;

    [Header("Ground Layer")]
    public LayerMask groundMask;

    [Header("Movement Checks")]
    public bool canMove;
    public bool isMoving;
    public bool isDashing;
    public bool canDash;
    public Vector3 currentVelocity;
    public Vector3 moveDir;
    public Image dashCooldown;
    private float timer;
    public float dashButton;
 

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
    [HideInInspector]
    public Color ogColor;

    [Header("Sound Effects")]

    public AudioSource sound;
    public AudioSource dashSound;
    public AudioClip meleeAttack;

    [Header("Cursor")]

    public Texture2D cursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("UI")]
    public Slider healthSlide;

    public float interactButton;
    public bool tDoorOpened;

    public bool dead;

    private void Awake()
    {
        input = new Controller();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Dash.performed += OnDashPerformed;
        input.Player.Dash.canceled += OnDashCancelled;
        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
    }

    private void OnDisable()
    {

        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Dash.performed -= OnDashPerformed;
        input.Player.Dash.canceled -= OnDashCancelled;
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
    }

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


        if (dm.isDialoguePlaying == true)
        {
            canMove = false;
          
        }
        else 
        {
            canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            MeleeAttack();
        }


        if (!dead)
        {
            AnimationHandler();
        }

        if (canMove == false)
        {
            _rb.velocity = Vector3.zero;
            moveDir = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        if (!isDashing && !dead && canMove == true)
        {
            _rb.velocity = moveDir * accelerationRate * Time.deltaTime;
        }

        if (dashButton == 1 && !isDashing && canDash && FindAnyObjectByType<OpenRuneMenu>().combinationUI.activeSelf == false && !dead && canMove)
        {
            _rb.AddForce(moveDir * dashSpeed * Time.deltaTime, ForceMode.Impulse);
            Invoke("StopDash", 0.2f);
            Invoke("DashCooldown", 1f);
            StartCoroutine(DashCooldownUI(1f));
            dashSound.time = 0;
            dashSound.Play();
            isDashing = true;
            canDash = false;
        }
    }


    public void OnMovementPerformed(InputAction.CallbackContext value)
    {

        if (canMove)
        {
            moveDir = value.ReadValue<Vector3>();
        }
    }

    public void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveDir = Vector3.zero;
    }

    public void OnDashPerformed(InputAction.CallbackContext value)
    {

        dashButton = value.ReadValue<float>();
    }

    public void OnDashCancelled(InputAction.CallbackContext value)
    {
        dashButton = value.ReadValue<float>();
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        interactButton = value.ReadValue<float>();
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {
        interactButton = value.ReadValue<float>();
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
        
    }

    public void StopDash()
    {
        //_rb.velocity = new Vector3(0, 0, 0);
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
        if (damageable && !CheatCodes.CheatsOn)
        {
            damageable = false;
            currentHealth = currentHealth - 1;
            if (currentHealth == 0)
            {
                dead = true;
                _rb.velocity = new Vector3(0, 0, 0);
                player.SetBool("isDead", true);
                sound.Play();
                Invoke("PlayerDeath", 3);
            }

            if (currentHealth > 0)
            {
                StartCoroutine(TakingDamageCooldown(0.15f));
                heartScript.TakeDamage();
            }
        }
    }

    public void PlayerDeath()
    {
        GameObject.Find("POC Manager").GetComponent<POCmanager>().PlayerRespawn();
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


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Doorswitch")
        {
            if (interactButton == 1 && !tDoorOpened)
            {
                _lm.CheckDoorOpen();
                tDoorOpened = true;
            }
        }
        if (other.gameObject.name == "GymTarget" && TutorialManager.tutorialStage == stage.Combat1Intro)
        {
            tm.CombatTutOne();
            _rb.velocity = Vector3.zero;
        }

        if (other.gameObject.name == "TutorialEndTrigger")
        {
            TutorialManager.tutorialStage = stage.Healing;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
