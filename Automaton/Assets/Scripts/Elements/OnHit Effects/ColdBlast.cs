using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBlast : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    private bool freezable;
    private Rigidbody _rb;

    private bool seconded;

    public AudioSource audioS; //tam
    public AudioClip iceBlastSFX; //tam

    // Start is called before the first frame update
    void Start()
    {
        freezable = true;
        _rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, 0, -10000);

        _rb.AddRelativeForce(force * Time.deltaTime, ForceMode.Impulse);
        
        audioS = GetComponent<AudioSource>(); //tam
    }

    private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SecondBlast()
    {
        _rb.isKinematic = true;
        _rb.velocity = new Vector3(0, 0, 0);
        firstObject.SetActive(false);
        Instantiate(secondObject, new Vector3(this.transform.position.x, secondObject.transform.position.y, this.transform.position.z), secondObject.transform.rotation);
        Destroy(this.gameObject);
        audioS.PlayOneShot(iceBlastSFX, 0.3f); //tam idk about the bottom section... 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7 && freezable)
        {
            if (other.gameObject.TryGetComponent<Goblin>(out Goblin gob))
            {
                gob.StartCrowdControl(1, 4, this.transform.position, false);
                gob.damageScript.TakeDamage(10, "");
            }
            else if (other.gameObject.TryGetComponent<RangeGoblin>(out RangeGoblin rGob))
            {
                rGob.StartCrowdControl(1, 4, this.transform.position, false);
                rGob.damageScript.TakeDamage(10, "");
            }
            else if (other.gameObject.TryGetComponent<SpecialRangedGoblin>(out SpecialRangedGoblin rsGob))
            {
                rsGob.StartCrowdControl(1, 4, this.transform.position, false);
                rsGob.damageScript.TakeDamage(10, "");
            }

            freezable = false;

            SecondBlast();
            seconded = true;
        }
    }
}
