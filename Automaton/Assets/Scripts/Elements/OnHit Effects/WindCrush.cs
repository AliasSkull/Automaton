using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WindCrush : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject secondBlast;
    public GameObject middle;

    public AudioSource audioS; //tam
    public AudioClip windCrushSFX; //tam
    
    // Start is called before the first frame update
    void Start()
    {
        ps.time = 0.90f;

        StartCoroutine(SecondBlast());

        audioS = GetComponent<AudioSource>(); //tam
    }

    public IEnumerator SecondBlast()
    {
        yield return new WaitForSeconds(2.2f);
        secondBlast.SetActive(true);
        middle.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        audioS.PlayOneShot(windCrushSFX, 0.3f); //tam

        if(secondBlast != null)
        {
            Destroy(secondBlast);
        }

        yield return new WaitForSeconds(1);

        if (middle != null)
        {
            Destroy(middle);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
