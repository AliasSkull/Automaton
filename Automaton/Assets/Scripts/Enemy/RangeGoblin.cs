using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RangeGoblin : MonoBehaviour
{
    public float attackSpeed;
    public Vector3 objectDirection;
    public float damageCount;
    public float stunTime;
    public float gobbySpeed;
    public int wave;
    public Damageable damageScript;
    public Animator anim;

    public Canvas goblinUI;
    public Image exlaimationP;

    public Rigidbody rb;

    public bool isWalking = true;
    public bool isAttacking;
    public bool noticed;
    private bool sliding;

    public GameObject player;
    public bool stunned;
    public bool pushedBack;

    public GameObject projectilePrefab;
    [SerializeField] private float timer = 5;
    private float bulletTime;

    public Transform spawnPoint;
    public Vector3 targetpos;
    public Quaternion rotateProjectile;

    private float angle;
    private Vector3 vecBet;

    //audio -- Tam added
    public AudioSource audioS;
    public AudioClip rangeGoblinAttack;
    public GameObject deathSound;
    public GameObject bloodSplat;

    // Start is called before the first frame update
    void Start()
    {
        exlaimationP.enabled = false;
        player = GameObject.Find("Player").transform.gameObject;

        rb = GetComponent<Rigidbody>();
        damageScript = GetComponentInChildren<Damageable>();

        isWalking = true;

        audioS = GetComponent<AudioSource>(); //Tam added
    }

    // Update is called once per frame
    void Update()
    {
        damageCount = GetComponentInChildren<Damageable>().damageCount;

        if (damageScript.currentHealth <= 0)
        {
            Death();
        }

        vecBet = this.transform.position - new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        angle = (Mathf.Atan2(vecBet.z, vecBet.x) * Mathf.Rad2Deg);
        AnimationHandler();
    }

    public void CreateProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(-vecBet.normalized * 7f, ForceMode.Impulse);
        audioS.PlayOneShot(rangeGoblinAttack, 0.3f); //Tam added
        Destroy(projectile, 4f);
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
        if (ccType == 2)
        {
            StartCoroutine(Push(pos, pushBack));
        }
        if (ccType == 3)
        {
            StartCoroutine(Slow(timer));
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

    public void Smackable()
    {
        if (rb.velocity.magnitude > 20f)
        {
            sliding = true;
        }
    }

    public IEnumerator Slow(float timer)
    {
        rb.isKinematic = true;
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        gobbySpeed /= 2.2f;
        yield return new WaitForSeconds(timer);
        gobbySpeed = 5;
        rb.isKinematic = false;
        //rb.constraints = RigidbodyConstraints.None;
    }

    public void FacePlayer() 
    {
        rb.velocity = new Vector3(0, 0, 0);
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
        if (sliding && collision.gameObject.layer != 8)
        {
            if (collision.gameObject.tag == "Damageable")
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

    public void StopAnimation()
    {
        isAttacking = false;
        anim.StopPlayback();
    }

    public void AnimationHandler()
    {
        if (!stunned)
        {
            if (angle >= -45 && angle <= 45)
            {
                anim.SetInteger("faceDir", 2);
            }
            else if (angle >= 45 && angle <= 145)
            {
                anim.SetInteger("faceDir", 0);
            }
            else if (angle >= 145 || angle <= -145)
            {
                anim.SetInteger("faceDir", 3);
            }
            else if (angle >= -145 || angle <= -45)
            {
                anim.SetInteger("faceDir", 1);
            }
        }
        
        anim.SetBool("Stunned", stunned);
        anim.SetBool("Chasing", isWalking);
        anim.SetBool("Attacking", isAttacking);
        anim.SetBool("Noticed", noticed);

        exlaimationP.enabled = noticed;

        //put direction changing below
    }

}
