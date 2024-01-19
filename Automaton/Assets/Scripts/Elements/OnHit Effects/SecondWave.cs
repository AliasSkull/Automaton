using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWave : MonoBehaviour
{
    private float timer;
    public float timeTillDeletion;
    private float lerpValue;

    private float startSize;
    private float endSize;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        startSize = 1;
        endSize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < timeTillDeletion)
        {
            lerpValue = Mathf.Lerp(startSize, endSize, timer / timeTillDeletion);
            this.transform.localScale = new Vector3(lerpValue, transform.position.y, lerpValue);
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damageable" && other.gameObject.layer == 7)
        {
            Goblin gob = other.gameObject.GetComponent<Goblin>();
            gob.StartCrowdControl(2, 0, this.transform.position);
            gob.damageScript.TakeDamage(0, "Push");
        }
    }
}
