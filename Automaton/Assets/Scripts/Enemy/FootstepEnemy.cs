using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepEnemy : MonoBehaviour
{
    public GameObject fireOrb;
    public AudioSource _as;
    
    // Start is called before the first frame update
    void Start()
    {
        fireOrb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstep()
    {
        _as.time = 0;
        _as.Play();
        
        if(StaticValues.flameLevel == 3)
        {
            float random = Random.Range(0f, 360f);
            GameObject fireStep = Instantiate(fireOrb, fireOrb.transform.position, Quaternion.Euler(fireOrb.transform.rotation.x, random, fireOrb.transform.rotation.y));
            fireStep.SetActive(true);

            if (CheatCodes.CheatsOn)
            {
                StartCoroutine(DestroyFlame(fireStep));
            }
        }
    }

    public IEnumerator DestroyFlame(GameObject flame)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(flame);
    }
}
