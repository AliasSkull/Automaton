using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRangedAttack : MonoBehaviour
{
    public GameObject hitbox;
    public MeshRenderer mr;
    public Material mA;
    public float timer;
    public float lerpValue;
    
    // Start is called before the first frame update
    void Start()
    {
        hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 2)
        {
            lerpValue = Mathf.Lerp(0, 1, timer / 2f);
            mr.materials[0].color = new Color(mr.materials[0].color.r, mr.materials[0].color.g, mr.materials[0].color.b, lerpValue);
            timer += Time.deltaTime;
        }
        else if (timer >= 2)
        {
            hitbox.SetActive(true);
            mr.enabled = false;
        }
        else if(timer >= 10)
        {
            Destroy(this.gameObject);
        }
    }

    
}
