using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public GameObject frozenPuddle;
    public GameObject freezeHitbox;
    public GameObject meltedPuddle;

    public bool freezing = false;

    public float timer;
    public float timeToMelt;

    // Start is called before the first frame update
    void Start()
    {
        frozenPuddle.SetActive(false);
        freezeHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (freezing)
        {
            Freeze();
        }
    }

    public void Freeze()
    {
        if (timer < timeToMelt)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= timeToMelt)
        {
            frozenPuddle.SetActive(false);
            meltedPuddle.SetActive(true);
            freezing = false;
        }
    }

    public void StopFreezeHitbox()
    {
        freezeHitbox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12 && other.gameObject.tag == "Ice" && !freezing)
        {
            frozenPuddle.SetActive(true);
            meltedPuddle.SetActive(false);
            freezeHitbox.SetActive(true);
            Invoke("StopFreezeHitbox", 0.2f);

            freezing = true;
        }
    }
}
