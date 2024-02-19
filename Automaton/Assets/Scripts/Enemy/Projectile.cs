using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 0.5f;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.TryGetComponent<PlayerController>(out PlayerController P);
            P.TakeDamage();
            
        }

        if(other.gameObject.layer == 6 || other.gameObject.layer == 13)
        {
            Destroy(this.gameObject);
        }
    }
}
