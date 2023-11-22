using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damageCount;

    public float damageTime;
    public float currentDamageTime;

    public GameObject Goblin;
    private SpriteRenderer sprite;
    private Color ogColor;

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.transform.parent.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        ogColor = sprite.color;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageCount > 0)
        {
            DamageCounter();
        }
    }

    public void TakeDamage(float damage, string extraText)
    {
        currentHealth = currentHealth - damage;
        damageCount = damageCount + 1;
        currentDamageTime = 0;

        if(damage > 0)
        {
            GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot").position, extraText + damage.ToString(), 1);
            StartRed();
        }
        else if(damage == 0)
        {
            GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot").position, extraText, 1);
        }


    }

    public void StartRed()
    {
        sprite.color = new Color(255, 0, 0);

        Invoke("StopRed", 0.1f);
    }

    public void StopRed()
    {
        sprite.color = ogColor;
    }

    public void DamageCounter() 
    {
        currentDamageTime += Time.deltaTime;

        if (currentDamageTime == damageTime)
        {
            damageCount = 0;
            currentDamageTime = 0;
        }
    }

}
