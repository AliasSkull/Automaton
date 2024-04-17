using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneChoser : MonoBehaviour
{
    public int gunIndex;

    public ElementManager EID;
    
    public Image combinationImg;
    
    public Image primaryIcon;
    public int primaryRune;

    public Image secondaryIcon;
    public int secondaryRune;

    public GameObject primarySelector;
    public GameObject secondarySelector;

    public TMP_Text comboText;
    public TMP_Text typeText;
    public TMP_Text descriptionBox;

    public List<Sprite> runeImages;

    public int runeCombo;

    public OpenRuneMenu _orm;
    public Image cooldownIcon;

    private void OnEnable()
    {
        primarySelector.SetActive(false);
        secondarySelector.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        primaryIcon.sprite = runeImages[primaryRune - 1];
        secondaryIcon.sprite = runeImages[secondaryRune - 1];
        combinationImg.sprite = runeImages[runeCombo + 3];
        comboText.text = EID.publicAccessElementDatabase.elements[runeCombo].name;
        typeText.text = EID.publicAccessElementDatabase.elements[runeCombo].spellType;
        descriptionBox.text = EID.publicAccessElementDatabase.elements[runeCombo].description;
    }

    public void ChangeRune(int index, bool primary)
    {
        if (primary)
        {
            switch (index)
            {
                case 1:
                    primaryIcon.sprite = runeImages[0];
                    primaryRune = 1;
                    break;
                case 2:
                    primaryIcon.sprite = runeImages[1];
                    primaryRune = 2;
                    break;
                case 3:
                    primaryIcon.sprite = runeImages[2];
                    primaryRune = 3;
                    break;
            }

            primarySelector.SetActive(false);
        }
        else if (!primary)
        {
            switch (index)
            {
                case 1:
                    secondaryIcon.sprite = runeImages[0];
                    secondaryRune = 1;
                    break;
                case 2:
                    secondaryIcon.sprite = runeImages[1];
                    secondaryRune = 2;
                    break;
                case 3:
                    secondaryIcon.sprite = runeImages[2];
                    secondaryRune = 3;
                    break;
            }

            secondarySelector.SetActive(false);
        }



        ChangeCombo();
    }

    public void ChangeCombo()
    {
        if(primaryRune == 1)
        {
            if(secondaryRune == 1)
            {
                combinationImg.sprite = runeImages[3];
                runeCombo = 0;
            }
            else if (secondaryRune == 2)
            {
                combinationImg.sprite = runeImages[6];
                runeCombo = 3;
            }
            else if (secondaryRune == 3)
            {
                combinationImg.sprite = runeImages[9];
                runeCombo = 6;
            }
        }
        else if(primaryRune == 2)
        {
            if (secondaryRune == 1)
            {
                combinationImg.sprite = runeImages[7];
                runeCombo = 4;
            }
            else if (secondaryRune == 2)
            {
                combinationImg.sprite = runeImages[4];
                runeCombo = 1;
            }
            else if (secondaryRune == 3)
            {
                combinationImg.sprite = runeImages[10];
                runeCombo = 7;
            }
        }
        else if(primaryRune == 3)
        {
            if (secondaryRune == 1)
            {
                combinationImg.sprite = runeImages[8];
                runeCombo = 5;
            }
            else if (secondaryRune == 2)
            {
                combinationImg.sprite = runeImages[11];
                runeCombo = 8;
            }
            else if (secondaryRune == 3)
            {
                combinationImg.sprite = runeImages[5];
                runeCombo = 2;
            }
        }

        comboText.text = EID.publicAccessElementDatabase.elements[runeCombo].name;
        typeText.text = EID.publicAccessElementDatabase.elements[runeCombo].spellType;
        descriptionBox.text = EID.publicAccessElementDatabase.elements[runeCombo].description;

        cooldownIcon.sprite = combinationImg.sprite;
        
        
        if (secondarySelector.activeSelf)
        {
            secondarySelector.SetActive(false);
        }
        if (primarySelector.activeSelf)
        {
            primarySelector.SetActive(false);
        }
    }
}
