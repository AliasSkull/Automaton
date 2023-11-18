using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Experimental.AI;

public class Goblin : MonoBehaviour
{
    public float attackSpeed;
    public Vector3 objectDirection;
    public float damageCount;
    public float stunTime;
    public Damageable damageScript;

    public Canvas goblinUI;
    public Image exlaimationP;

    public Collider hitbox;
    public Rigidbody rb;

    private bool readyAttack;
    private bool isAttacking;
    private bool sliding;


    public Animator goblinAnimator;

    public AudioSource audioS;
    public AudioClip goblinSees;
    public AudioClip goblinAttack;

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
        exlaimationP.enabled = false;
        audioS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        damageCount = GetComponentInChildren<Damageable>().damageCount;

        if (damageScript.currentHealth <= 0)
        {
            Death();
        }

        AnimationHandler();

        if (sliding)
        {
            print(rb.velocity.magnitude);
        }

    }

    public void Attack() 
    {
       // if (!readyAttack || isAttacking) return;

        readyAttack = false;
        isAttacking = true;

        goblinAnimator.SetBool("isAttacking", true);
        rb.AddForce(Vector3.forward * 2, ForceMode.Impulse);
        hitbox.enabled = true;
        audioS.PlayOneShot(goblinAttack, 0.3f);
  
        
        Invoke(nameof(ResetAttack), attackSpeed);


    }

    public void ResetAttack() 
    {
        isAttacking = false;
        readyAttack = true;
        hitbox.enabled = false; 
        goblinAnimator.SetBool("isAttacking", false);

    }

    public void ShowExclaimation() 
    {
        exlaimationP.enabled = true;
        audioS.PlayOneShot(goblinSees, 0.5f);
        StartCoroutine(UICountdown());
    }

    public IEnumerator Stun(float stunT)
    {
        stunned = true;
        yield return new WaitForSeconds(stunT);
        stunned = false;
    }

    public void StartCrowdControl(int ccType, float timer, Vector3 pos)
    {
        if (ccType == 1)
        {
            StartCoroutine(Stun(timer));
        }
        if(ccType == 2)
        {
            StartCoroutine(Push(pos));
        }
        

    }

    public void Smackable()
    {
        if (rb.velocity.magnitude > 20f)
        {
            sliding = true;
        }
    }

    public IEnumerator Push(Vector3 pushedFromPos)
    {
        Vector3 vectorBetwixt = this.transform.position - pushedFromPos;

        Invoke("Smackable", 0.1f);
        rb.AddForce(vectorBetwixt.normalized * 80, ForceMode.Impulse);
        stunned = true;
        yield return new WaitForSeconds(0.5f);
        stunned = false;
        sliding = false;
        rb.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator UICountdown() 
    {
        yield return new WaitForSeconds(1);
        exlaimationP.enabled = false;
        StopCoroutine(UICountdown());
    }


    public void AnimationHandler() 
    {
        if (rb.velocity.x != 0 || rb.velocity.z != 0)
        {
            goblinAnimator.SetBool("isWalking", true);
        }
        else 
        {
            goblinAnimator.SetBool("isWalking", false);
        }
    
    }

    public void Death() 
    {

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damageable" && sliding)
        {
            damageScript.TakeDamage(5, "");
            collision.gameObject.GetComponent<Goblin>().damageScript.TakeDamage(5, "");
            sliding = false;
        }
    }
}
