using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneChoser : MonoBehaviour
{
    private Selector slctr;
    private RuneIcon rI; 

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





            runeMenuSelector.clicked = false;
            runeMenu.SetActive(false);
        }
    }

    public void OpenRuneMenu()
    {
        runeMenu.SetActive(true);
    }
}
