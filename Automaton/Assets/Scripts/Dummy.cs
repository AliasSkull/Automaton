using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using UnityEditor.Animations;

public class Dummy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Animator anim;
    public AudioSource barrelAudio;
    public AudioClip destroy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            anim.SetBool("isDead", true);
           // barrelAudio.PlayOneShot(destroy);
            StartCoroutine("WaitforDeath");
         
        }
    }

    public void TakeDamage(float damage) 
    {
        Debug.Log(this.gameObject.name + "took damage");
        currentHealth = currentHealth - damage;
    }

    IEnumerator WaitforDeath()
    {
  
        yield return new WaitForSeconds(0.5f);

        Death();

    }

    public void Death() 
    {
        Destroy(this.gameObject);
    }
}
