using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxLife = 2;
    public float currentLife;

    public GameObject player;
    public PlayerController playerControl;

    public float speed = 0.5f;

 

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerController>();
       

      
    }

    // Update is called once per frame
    void Update()
    {
       
        //rb.AddForce(this.transform.forward * 15);
       // Destroy(this.gameObject, 1f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Ranged Damage");
            playerControl.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
