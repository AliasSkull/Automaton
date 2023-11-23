using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxLife = 3;
    public float currentLife;

    public GameObject player;
    public PlayerController playerControl;

    public float speed = 2f;

    public Vector3 targetPOS;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        player = GameObject.FindGameObjectWithTag("Player");
       

        targetPOS = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        currentLife -= Time.deltaTime;

        if (currentLife <= 0)
        {
            Death();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed); 

    }

    public void Death() 
    {
        Destroy(this.gameObject);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage(2);
            Death();
            
        }
    }
}
