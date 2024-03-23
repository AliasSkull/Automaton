using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerButtonChecks : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InputManager _im;
    public Button butt;
    
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _im.currentlySelected = butt;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _im.currentlySelected = null;
    }
}
