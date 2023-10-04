using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneChoser : MonoBehaviour
{
    private Selector slctr;

    public GameObject runeMenu;
    public Selector runeMenuSelector;


    
    // Start is called before the first frame update
    void Start()
    {
        slctr = GetComponent<Selector>();
        runeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (slctr.clicked)
        {
            OpenRuneMenu();
        }

        if (runeMenuSelector.clicked)
        {
            string selectedRuneIconID = runeMenuSelector.currentHoveredIcon.transform.GetChild(0).gameObject.GetComponent<RuneIcon>().iconIndex;

            RuneIcon rI = slctr.currentHoveredIcon.transform.GetChild(0).gameObject.GetComponent<RuneIcon>();
            rI.iconIndex = selectedRuneIconID;
            rI.SetIcon();



            slctr.clicked = false;
            slctr.IconClickClose();
            runeMenuSelector.clicked = false;
            runeMenu.SetActive(false);
        }
    }

    public void OpenRuneMenu()
    {
        runeMenu.SetActive(true);
    }
}
