using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDummy : MonoBehaviour
{
    public Damageable damage;
    public Rigidbody rb;

    public bool stunned;

    // Start is called before the first frame update
    void Start()
    {
      
      
    }

    // Update is called once per frame
    void Update()
    {
        if (damage.currentHealth == 0)
        {
            Death();
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


    public void Death()
    {
        FindAnyObjectByType<DummySpawn>().DummyList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stunned && collision.gameObject.layer != 8 && collision.gameObject.tag != "Ground")
        {
            damage.TakeDamage(5,1);
        }

    }
}
