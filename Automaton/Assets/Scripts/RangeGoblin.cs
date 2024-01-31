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
    public Damageable damageScript;

    public Canvas goblinUI;
    public Image exlaimationP;

    public Collider hitbox;
    public Rigidbody rb;

    private bool readyAttack;
    private bool isAttacking;
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
        hitbox.enabled = false;
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



    }

    public void Attack() 
    {

        //Debug.Log("Range Attack");
        readyAttack = false;
        isAttacking = true;

    
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(transform.forward * 15f, ForceMode.Impulse);
        Destroy(projectile, 2f);

    }

    public void FacePlayer() 
    {
        transform.LookAt(player.transform.position);
    }

    void ResetAttack()
    {
        isAttacking = false;
        readyAttack = true;
      

    }

    public void ShowExclaimation()
    {
        exlaimationP.enabled = true;
        Invoke("UnShowUI", 1);
    }

    public void UnShowUI()
    {
        exlaimationP.enabled = false;
    }

    public void Death() 
    { 
    
    
    }

}
