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
        timer += Time.deltaTime;
        
        if (timer < 1.1f)
        {
            lerpValue = Mathf.Lerp(0, 1, timer / 2f);
            mr.materials[0].color = new Color(mr.materials[0].color.r, mr.materials[0].color.g, mr.materials[0].color.b, lerpValue);

        }
        else if (timer >= 1.1f)
        {
            hitbox.SetActive(true);
            mr.enabled = false;
        }
        
        if(timer >= 7)
        {
            Destroy(this.gameObject);
        }
    }

    
}
