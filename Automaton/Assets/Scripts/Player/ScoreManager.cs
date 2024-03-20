using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public PlayerController _pc;
    public TMPro.TextMeshProUGUI scoreCounter;
    public TMPro.TextMeshProUGUI scoreCountUpUI;
    public Image heatGauge;
    public SpriteRenderer playerSprite;
    public Animator engineAnim;

    private float timer;
    private float overallTimer;

    private bool timerStarted = false;
    private bool cooling;

    private float scoreCountUp;
    private float scoreMultiplier;
    public List<int> currentlyUsedDamageTypes;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(CheatCodes.CheatsOn && Input.GetKeyDown("="))
        {
            StaticValues.flameLevel++;
        }
        
        if(scoreCounter != null)
        {
            scoreCounter.text = StaticValues.score.ToString();
        }

        if (timerStarted)
        {
            timer += Time.deltaTime;
            //overallTimer += Time.deltaTime;
            scoreCountUpUI.text = "+" + ((int)(scoreCountUp * scoreMultiplier)).ToString();

            if(timer > 1f)
            {
                heatGauge.fillAmount = 1 - (timer / 5);
                cooling = true;
            }
            else
            {
                heatGauge.fillAmount = 1;
                cooling = false;
            }
        }

        if(timer >= 5)
        {
            timer = 0;
            overallTimer = 0;
            timerStarted = false;
            cooling = false;
            scoreCountUpUI.text = "";
            heatGauge.fillAmount = 0;
            StaticValues.flameLevel = 0;
            SetFinalScore();
            print("done");
        }

        if(overallTimer >= 10 && overallTimer <= 20)
        {
            StaticValues.flameLevel = 1;
        }
        
        if (overallTimer >= 25 && overallTimer <= 40)
        {
            StaticValues.flameLevel = 2;
        }

        if (overallTimer >= 40)
        {
            StaticValues.flameLevel = 3;
        }

        engineAnim.SetBool("Cooling", cooling);
        engineAnim.SetBool("Heating", timerStarted);
    }

    public void ScoreUp(float damage, int damageType)
    {
        timer = 0;
        if (!timerStarted)
        {
            timerStarted = true;
            scoreCountUp = 0;
            scoreMultiplier = 1;
        }

        scoreCountUp += (int)damage;
        
        bool used = false;
        
        foreach(int type in currentlyUsedDamageTypes)
        {
            if(type == damageType)
            {
                used = true;
            }
        }

        if (!used) //new damage, meaning add to multiplier
        {
            scoreMultiplier += 0.1f;
            currentlyUsedDamageTypes.Add(damageType);
        }
    }

    public void SetFinalScore()
    {
        StaticValues.score += (int) (scoreCountUp * scoreMultiplier);
    }

}
