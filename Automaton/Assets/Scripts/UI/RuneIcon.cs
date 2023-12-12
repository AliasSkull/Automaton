using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneIcon : MonoBehaviour
{
    public RuneChoser runeChoser;
    public int RuneIndex;
    public bool primarySelector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void IconSelected()
    {
        runeChoser.ChangeRune(RuneIndex, primarySelector);
    }

}
