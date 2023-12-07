using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenRuneMenu : MonoBehaviour
{
    public PlayerAimer playerAimScript;
    public TextMeshProUGUI spellText;
    public TextMeshProUGUI spellText2;

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
            
        }
        else if (Input.GetKeyDown("2"))
        {
            ChangeRune(1);

        }
        else if (Input.GetKeyDown("3"))
        {
            ChangeRune(2);

        }
        else if (Input.GetKeyDown("4"))
        {
            ChangeRune(3);

        }
        else if (Input.GetKeyDown("5"))
        {
            ChangeRune(4);

        }
        else if (Input.GetKeyDown("6"))
        {
            ChangeRune(5);

        }
        else if (Input.GetKeyDown("7"))
        {
            ChangeRune(6);

        }
        else if (Input.GetKeyDown("8"))
        {
            ChangeRune(7);

        }
        else if (Input.GetKeyDown("9"))
        {
            ChangeRune(8);

        }
    }

    public void ChangeRune(int rune)
    {
        if (Input.GetKey("left shift"))
        {
            playerAimScript.SetElement(2, rune);
            print("stuff");
            spellText2.text = "Spell " + (rune+ 1).ToString();
        }
        else
        {
            playerAimScript.SetElement(1, rune);
            spellText.text = "Spell " + (rune + 1).ToString();
        }
        

    }
}
