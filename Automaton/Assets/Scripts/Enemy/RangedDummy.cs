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

    private float angle;
    private Vector3 vecBet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z + 2);
    }

    // Update is called once per frame
    void Update()
    {
        vecBet = this.transform.position - new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        angle = (Mathf.Atan2(vecBet.z, vecBet.x) * Mathf.Rad2Deg);

        timePassed += Time.deltaTime;
        if (FindAnyObjectByType<DialogueManager>().isDialoguePlaying == false && timePassed > 3F)
        {
            CreateProjectile();
            timePassed = 0;
        }
        
    }

    public void CreateProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.identity) as GameObject;
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(-vecBet.normalized * 7f, ForceMode.Impulse);
        audioS.PlayOneShot(rangeGoblinAttack, 0.3f); //Tam added
        Destroy(projectile, 4f);
    }
 
}
