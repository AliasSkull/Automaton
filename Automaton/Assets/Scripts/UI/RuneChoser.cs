using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneChoser : MonoBehaviour
{
    private Selector slctr;
    public GameObject runeMenu;
    
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
    }

    public void OpenRuneMenu()
    {
        runeMenu.SetActive(true);
    }
}
