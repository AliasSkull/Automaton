using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    private GameObject selector;
    private GameObject runeMenu;
    private Image selectorImg;
    [System.NonSerialized]
    public GameObject currentHoveredIcon;
    public bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        selector = this.transform.GetChild(0).gameObject;
        selector.SetActive(false);
        selectorImg = selector.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IconHovered(GameObject icon)
    {
        currentHoveredIcon = icon;
        if (!clicked)
        {
            selector.SetActive(true);
            selectorImg.color = Color.yellow;
            selector.transform.position = icon.transform.position;
            selector.transform.localScale = icon.transform.localScale * 1.2f;
        }
    }

    public void IconExited()
    {
        if(!clicked)
        selector.SetActive(false);
    }

    public void IconClicked()
    {
        selectorImg.color = Color.green;
        clicked = true;
    }

    public void IconClickClose()
    {
        selector.SetActive(false);
    }


}
