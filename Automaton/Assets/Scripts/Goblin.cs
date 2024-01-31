using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Experimental.AI;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    public float attackSpeed;
    public Vector3 objectDirection;
    public LayerMask gobbiesSocialDistanceLayerMask;
    public float damageCount;
    public float stunTime;
    public float gobbySpeed;
    public Damageable damageScript;

    public Canvas goblinUI;
    public Image exlaimationP;

    public Collider hitbox;
    public Rigidbody rb;
    public NavMeshAgent nma;

    private bool readyAttack;
    private bool isAttacking;
    private bool sliding;
    public bool chasing;
    private float angle;
    public bool isWalking = true;

    public Animator goblinAnimator;

    public AudioSource audioS;
    public AudioClip goblinSees;
    public AudioClip goblinAttack;

    public GameObject player;
    public GameObject bloodSplat;
    public bool stunned;
    public bool pushedBack;

    public Collider[] gobbiesInSocialDistanceBubble;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.gameObject;
        isWalking = true;
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

        GobbiesInArea();

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

    public void GobbiesInArea()
    {
        gobbiesInSocialDistanceBubble = Physics.OverlapSphere(this.transform.position, 3, gobbiesSocialDistanceLayerMask);

        foreach(Collider coll in gobbiesInSocialDistanceBubble)
        {
            if(coll.gameObject != this.gameObject)
            {
                SocialDistance(coll.transform);
            }
        }
    }

    public void SocialDistance(Transform gobInBubble)
    {
        Vector3 vectorAwayFrom = this.transform.position - gobInBubble.position;
        rb.AddForce(vectorAwayFrom.normalized * Time.deltaTime * vectorAwayFrom.magnitude * 150);
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

    public void StartCrowdControl(int ccType, float timer, Vector3 pos, bool pushBack)
    {
        if (ccType == 1)
        {
            StartCoroutine(Stun(timer));
        }
        if(ccType == 2)
        {
            StartCoroutine(Push(pos, pushBack));
        }
        if(ccType == 3)
        {
            StartCoroutine(Slow(timer));
        }

    }

    public void Smackable()
    {
        if (rb.velocity.magnitude > 20f)
        {
            sliding = true;
        }
    }

    public IEnumerator Push(Vector3 pushedFromPos, bool pushBack)
    {
        Vector3 vectorBetwixt = this.transform.position - pushedFromPos;

        if (!pushBack)
        {
            vectorBetwixt = pushedFromPos - this.transform.position;
        }

        Invoke("Smackable", 0.1f);
        rb.AddForce(vectorBetwixt.normalized * 100, ForceMode.Impulse);
        stunned = true;
        yield return new WaitForSeconds(0.5f);
        stunned = false;
        sliding = false;
        rb.velocity = new Vector3(0, 0, 0);
    }

    public IEnumerator Slow(float timer)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        gobbySpeed /= 3f;
        yield return new WaitForSeconds(timer);
        gobbySpeed = 5;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
    }

    IEnumerator UICountdown() 
    {
        yield return new WaitForSeconds(1);
        exlaimationP.enabled = false;
        StopCoroutine(UICountdown());
    }


    public void AnimationHandler() 
    {
        goblinAnimator.SetBool("isWalking", isWalking);

        if (chasing)
        {
            Vector3 vectorBetween = this.transform.position - new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            angle = (Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);

            if (angle >= -45 && angle <= 45)
            {
                goblinAnimator.SetInteger("faceDir", 2);
            }
            else if (angle >= 45 && angle <= 145)
            {
                goblinAnimator.SetInteger("faceDir", 0);
            }
            else if (angle >= 145 || angle <= -145)
            {
                goblinAnimator.SetInteger("faceDir", 3);
            }
            else if (angle >= -145 || angle <= -45)
            {
                goblinAnimator.SetInteger("faceDir", 1);
            }
        }
        else
        {
            float gobYRotation = transform.rotation.eulerAngles.y;

            if (gobYRotation > 180)
            {
                gobYRotation -= 360;
            }

            if (gobYRotation >= -45 && gobYRotation <= 45)
            {
                goblinAnimator.SetInteger("faceDir", 1);
            }
            else if (gobYRotation >= 45 && gobYRotation <= 145)
            {
                goblinAnimator.SetInteger("faceDir", 3);
            }
            else if (gobYRotation >= 145 || gobYRotation <= -145)
            {
                goblinAnimator.SetInteger("faceDir", 0);
            }
            else if (gobYRotation >= -145 || gobYRotation <= -45)
            {
                goblinAnimator.SetInteger("faceDir", 2);
            }
        }
    }

    public void Death() 
    {
        if (CheatCodes.CheatsOn)
        {
            GameObject blood = Instantiate(bloodSplat, new Vector3(this.transform.position.x, bloodSplat.transform.position.y, this.transform.position.z), bloodSplat.transform.rotation);
        }

        GameObject.Find("EnemySpawningManager").GetComponent<EnemySpawningManager>().EnemyDeathReset(this.gameObject);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 2);
    }

}
