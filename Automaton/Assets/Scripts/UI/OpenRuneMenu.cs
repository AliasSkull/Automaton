using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenRuneMenu : MonoBehaviour
{
    public PlayerAimer playerAimScript;
    public TextMeshProUGUI spellText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ChangeRune(0);
            spellText.text = "Spell 1";
        }
        else if (Input.GetKeyDown("2"))
        {
            ChangeRune(1);
            spellText.text = "Spell 2";
        }
        else if (Input.GetKeyDown("3"))
        {
            ChangeRune(2);
            spellText.text = "Spell 3";
        }
        else if (Input.GetKeyDown("4"))
        {
            ChangeRune(3);
            spellText.text = "Spell 4";
        }
        else if (Input.GetKeyDown("5"))
        {
            ChangeRune(4);
            spellText.text = "Spell 5";
        }
        else if (Input.GetKeyDown("6"))
        {
            ChangeRune(5);
            spellText.text = "Spell 6";
        }
        else if (Input.GetKeyDown("7"))
        {
            ChangeRune(6);
            spellText.text = "Spell 7";
        }
        else if (Input.GetKeyDown("8"))
        {
            ChangeRune(7);
            spellText.text = "Spell 8";
        }
        else if (Input.GetKeyDown("9"))
        {
            ChangeRune(8);
            spellText.text = "Spell 9";
        }
    }

    public void ChangeRune(int rune)
    {
        playerAimScript.SetElement(rune);
    }
}
