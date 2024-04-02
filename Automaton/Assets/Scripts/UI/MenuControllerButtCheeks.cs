using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuControllerButtCheeks : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuInputManager _mim;
    public Button butt;

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _mim.currentlySelected = butt;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _mim.currentlySelected = null;
    }
}
