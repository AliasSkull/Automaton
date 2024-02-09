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

    private bool isWalking = true;
    public bool isAttacking;
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

    // Start is called before the first frame update
    void Start()
    {
        exlaimationP.enabled = false;
        player = GameObject.Find("Player").transform.gameObject;

        rb = GetComponent<Rigidbody>();
        damageScript = GetComponentInChildren<Damageable>();

        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        damageCount = GetComponentInChildren<Damageable>().damageCount;

        if (damageScript.currentHealth <= 0)
        {
            Death();
        }

        Vector3 vectorBetween = new Vector3(transform.position.x, transform.position.y, transform.position.z) - new Vector3(player.transform.position.x, 0, player.transform.position.z);
         float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
        rotateProjectile = Quaternion.Euler(0, rotation, 0);

        AnimationHandler();
    }

    public void Attack() 
    {
        isAttacking = true;
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(transform.forward * 15f, ForceMode.Impulse);
        Destroy(projectile, 2f);

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
        isAttacking = true;
        transform.LookAt(player.transform.position);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void ShowExclaimation()
    {
        exlaimationP.enabled = true;

        print("bruh");
        Invoke("UnShowUI", 1);
    }

    public void UnShowUI()
    {
        exlaimationP.enabled = false;
    }

    public void Death() 
    {
        GameObject.Find("EnemySpawningManager").GetComponent<EnemySpawningManager>().EnemyDeathReset(this.gameObject, wave);
    }

    public void AnimationHandler()
    {
        anim.SetBool("Chasing", isWalking);
        anim.SetBool("Attacking", isAttacking);
        //put direction changing below
    }

}
