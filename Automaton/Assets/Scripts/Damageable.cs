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
    public ScoreManager _sm;
    private SpriteRenderer sprite;
    private Color ogColor;

    public bool isNotGoblin;

    // Start is called before the first frame update
    void Start()
    {
        _sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        if (isNotGoblin == false)
        {
            sprite = this.transform.parent.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
            ogColor = sprite.color;
        }
       
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

    public void TakeDamage(float damage, int damageType)
    {
        currentHealth = currentHealth - damage;
        damageCount = damageCount + 1;
        currentDamageTime = 0;

        GameObject.Find("DamageNumberManager").GetComponent<DamageNumberChecker>().DamageTextShower1000(this.transform.parent.Find("DamageTextSpot"), damage.ToString(), 1);

        if (isNotGoblin == false)
        {

            _sm.ScoreUp(damage, damageType);
            StartRed();
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
