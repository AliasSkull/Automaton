using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDummy : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    [SerializeField] private float timer = 5;
    private float bulletTime;
    public Vector3 spawnPoint;
    public AudioSource audioS;
    public AudioClip rangeGoblinAttack;
    private float timePassed;

    public Damageable damage;
    public Rigidbody rb;

    public bool stunned;

    private float angle;
    private Vector3 vecBet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z + 2);


        if (damage.currentHealth <= 0)
        {
            Death();
        }


        vecBet = this.transform.position - new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        angle = (Mathf.Atan2(vecBet.z, vecBet.x) * Mathf.Rad2Deg);

        timePassed += Time.deltaTime;
        if (FindAnyObjectByType<DialogueManager>().isDialoguePlaying == false && timePassed > 3F)
        {
            CreateProjectile();
            timePassed = 0;
        }
        
    }

    public void Push(Vector3 pushedFromPos, bool pushBack)
    {
        StartCoroutine(Pushback(pushedFromPos, pushBack));
    }

    public IEnumerator Pushback(Vector3 pushedFromPos, bool pushBack)
    {
        Vector3 vectorBetwixt = this.transform.position - pushedFromPos;

        if (!pushBack)
        {
            vectorBetwixt = pushedFromPos - this.transform.position;
        }

        //Invoke("Smackable", 0.1f);
        rb.AddForce(vectorBetwixt.normalized * 100, ForceMode.Impulse);
        stunned = true;

        yield return new WaitForSeconds(0.5f);

        stunned = false;
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void CreateProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.identity) as GameObject;
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(-vecBet.normalized * 7f, ForceMode.Impulse);
        audioS.PlayOneShot(rangeGoblinAttack, 0.3f); //Tam added
        Destroy(projectile, 4f);
    }

    public void Death()
    {
        FindAnyObjectByType<DummySpawn>().DummyList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stunned && collision.gameObject.layer != 8 && collision.gameObject.tag != "Ground")
        {
            damage.TakeDamage(5, 1);
        }

    }

}
