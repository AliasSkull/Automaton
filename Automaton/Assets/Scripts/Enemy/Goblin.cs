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
    public int wave;
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
    public GameObject deathSound;

    public GameObject player;
    public GameObject bloodSplat;
    public bool stunned;
    public bool pushedBack;

    public Collider[] gobbiesInSocialDistanceBubble;

    private int startFaceDir;
    private float lerpValue;
    private bool startingAnim;
    private float startAnimTimer;
    private bool startStop;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAnimation();

        player = GameObject.Find("Player").transform.gameObject;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        damageScript = GetComponentInChildren<Damageable>();
        hitbox.enabled = false;
        exlaimationP.enabled = false;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startingAnim)
        {
            rb.isKinematic = false;
            damageCount = GetComponentInChildren<Damageable>().damageCount;

            if (damageScript.currentHealth <= 0)
            {
                Death();
            }

            GobbiesInArea();

            AnimationHandler();
        }
        else if(startAnimTimer <= 1.5f)
        {
            if (startFaceDir == 0)
            {
                lerpValue = Mathf.Lerp(this.transform.position.z, this.transform.position.z - 0.1f, startAnimTimer / 1.5f);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, lerpValue);
            }
            else if (startFaceDir == 2)
            {
                lerpValue = Mathf.Lerp(this.transform.position.x, this.transform.position.x - 0.1f, startAnimTimer / 1.5f);
                this.transform.position = new Vector3(lerpValue, this.transform.position.y, this.transform.position.z);
            }
            else if (startFaceDir == 3)
            {
                lerpValue = Mathf.Lerp(this.transform.position.x, this.transform.position.x + 0.1f, startAnimTimer / 1.5f);
                this.transform.position = new Vector3(lerpValue, this.transform.position.y, this.transform.position.z);
            }

            startAnimTimer += Time.deltaTime;
        }

        if(startAnimTimer > 1.5 && !startStop)
        {
            startingAnim = false;
            startStop = true;
        }
    }

    public void SpawnAnimation()
    {
        startingAnim = true;

        if(transform.parent.tag == "Down")
        {
            startFaceDir = 0;
            print("down");
        }
        else if (transform.parent.tag == "Left")
        {
            startFaceDir = 2;
            print("left");
        }
        else if (transform.parent.tag == "Right")
        {
            startFaceDir = 3;
            print("right");
        }
        else
        {
            startFaceDir = 0;
        }

        goblinAnimator.SetInteger("faceDir", startFaceDir);
        this.transform.SetParent(null);

        isWalking = true;
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
        gobbiesInSocialDistanceBubble = Physics.OverlapSphere(this.transform.position, 2, gobbiesSocialDistanceLayerMask);

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
        rb.velocity = new Vector3(0, 0, 0);
        goblinAnimator.SetBool("Noticed", true);
        chasing = true;

        exlaimationP.enabled = true;
        audioS.PlayOneShot(goblinSees, 0.5f);
        StartCoroutine(UICountdown());
    }

    public IEnumerator Stun(float stunT)
    {
        stunned = true;
        rb.mass = 100000;
        rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(stunT);
        rb.mass = 3;
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
        pushedBack = true;
        
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
        pushedBack = false;
        rb.velocity = new Vector3(0, 0, 0);
    }

    public IEnumerator Slow(float timer)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        gobbySpeed /= 2.2f;
        yield return new WaitForSeconds(timer);
        gobbySpeed = 5;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
    }

    IEnumerator UICountdown() 
    {
        yield return new WaitForSeconds(2);
        exlaimationP.enabled = false;
        goblinAnimator.SetBool("Noticed", false);
        StopCoroutine(UICountdown());
    }


    public void AnimationHandler() 
    {
        goblinAnimator.SetBool("isWalking", isWalking);
        goblinAnimator.SetBool("Stunned", stunned);

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
            Instantiate(bloodSplat, new Vector3(this.transform.position.x, bloodSplat.transform.position.y, this.transform.position.z), bloodSplat.transform.rotation);
        }
        Instantiate(deathSound, this.transform.position, this.transform.rotation);
        GameObject.Find("EnemySpawningManager").GetComponent<EnemySpawningManager>().EnemyDeathReset(this.gameObject, wave);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sliding && collision.gameObject.layer != 8 && collision.gameObject.tag != "Ground")
        {
            if (collision.gameObject.tag == "Damageable" )
            {
                damageScript.TakeDamage(10, 16);

                if (collision.gameObject.TryGetComponent<Goblin>(out Goblin gob))
                {
                    gob.damageScript.TakeDamage(10, 16);
                }
                else if (collision.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
                {
                    rGob.damageScript.TakeDamage(10, 16);
                }
                else if (collision.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin srGob))
                {
                    srGob.damageScript.TakeDamage(10, 16);
                }

                sliding = false;
            }
            else
            {
                damageScript.TakeDamage(5, 17);
                print(collision.gameObject);
                sliding = false;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 2);
    }

}
