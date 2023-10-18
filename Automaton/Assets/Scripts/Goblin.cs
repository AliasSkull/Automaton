using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using JetBrains.Annotations;

public class Goblin : MonoBehaviour
{

    public float attackSpeed;
    public Vector3 objectDirection;
    public float damageCount;
    public Damageable damageScript;

    public Canvas goblinUI;
    public Image exlaimationP;

    public Collider hitbox;
    public Rigidbody rb;

    private bool readyAttack;
    private bool isAttacking;

    public Animator goblinAnimator;

    public AudioSource audioS;
    public AudioClip goblinSees;
    public AudioClip goblinAttack;
   
    // Start is called before the first frame update
    void Start()
    {
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
}
