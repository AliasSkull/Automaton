using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject explosion;
    public GameObject barrelVisual;

    public bool exploding = false;

    public float timer;
    public float timeToExplode;
    private float startSize;
    private float endSize;
    private float lerpValue;

    // Start is called before the first frame update
    void Start()
    {
        explosion.SetActive(false);

        startSize = explosion.transform.localScale.x;
        endSize = startSize *5;
    }

    // Update is called once per frame
    void Update()
    {
        if (exploding)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if(timer < timeToExplode)
        {
            lerpValue = Mathf.Lerp(startSize, endSize, timer / timeToExplode);
            explosion.transform.localScale = new Vector3(lerpValue, lerpValue, lerpValue);
            timer += Time.deltaTime;
        }
        else if(timer >= timeToExplode)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12)
        {
            explosion.SetActive(true);
            barrelVisual.SetActive(false);
            exploding = true;
        }
    }
}
