using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public PlayerController _pc;
    public TMPro.TextMeshProUGUI scoreCounter;
    public GameObject fireRing;
    public GameObject bonfire;
    public GameObject smoke;
    public SpriteRenderer playerSprite;

    private Color ogColor;
    public Color fireColor1;
    public Color fireColor2;
    public Color fireColor3;

    private int prevFlameLevel;


    // Start is called before the first frame update
    void Start()
    {
        ogColor = playerSprite.color;
        prevFlameLevel = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (prevFlameLevel != StaticValues.flameLevel)
        {
            ChangeFlame();
            prevFlameLevel = StaticValues.flameLevel;
        }
        
        if(CheatCodes.CheatsOn && Input.GetKeyDown("="))
        {
            StaticValues.flameLevel++;
        }
        
        if(scoreCounter != null)
        {
            scoreCounter.text = StaticValues.score.ToString();
        }
        
        
    }

    public void ChangeFlame()
    {
        if (StaticValues.flameLevel >= 1)
        {
            smoke.SetActive(true);
            playerSprite.color = fireColor1;
            _pc.ogColor = fireColor1;
        }

        if (StaticValues.flameLevel >= 2)
        {
            bonfire.SetActive(true);
            playerSprite.color = fireColor2;
            _pc.ogColor = fireColor2;
        }

        if (StaticValues.flameLevel >= 3)
        {
            fireRing.SetActive(true);
            playerSprite.color = fireColor3;
            _pc.ogColor = fireColor3;
        }

        if (StaticValues.flameLevel == 0)
        {
            fireRing.SetActive(false);
            bonfire.SetActive(false);
            smoke.SetActive(false);

            playerSprite.color = ogColor;
            _pc.ogColor = ogColor;
        }
    }

}
