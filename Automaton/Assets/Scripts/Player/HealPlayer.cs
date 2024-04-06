using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public AudioSource _as;
    public bool healed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14 && other.gameObject.tag == "Player" && !healed)
        {
            other.gameObject.GetComponent<PlayerController>().HealPlayer();
            _as.time = 7.9f;
            _as.Play();
            healed = true;
            Invoke("DestroyMyself", 5f);
        }
    }
}
