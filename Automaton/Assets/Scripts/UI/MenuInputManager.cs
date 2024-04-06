using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{
    public Button currentlySelected;
    public Button[] buttons;

    public Controller input;

    public Vector2 mousePosition = new Vector2(Screen.width / 2, Screen.height /2);
    public Vector2 aimInput;

    private void Awake()
    {
        input = new Controller();
        StaticValues.controller = true;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
        input.Player.Aim.performed += OnAimPerformed;
        input.Player.Aim.canceled += OnAimCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
        input.Player.Aim.performed -= OnAimPerformed;
        input.Player.Aim.canceled -= OnAimCancelled;
    }

    // Start is called before the first frame update
    void Start()
    {
        buttons = Object.FindObjectsOfType<Button>();

        foreach (Button butt in buttons)
        {
            MenuControllerButtCheeks cButtCheeks = butt.gameObject.AddComponent<MenuControllerButtCheeks>();
            cButtCheeks._mim = this;
            cButtCheeks.butt = butt;
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        if (currentlySelected != null)
        {
            currentlySelected.onClick.Invoke();
        }
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {

    }

    public void OnAimPerformed(InputAction.CallbackContext value)
    {
        aimInput = value.ReadValue<Vector3>();
    }

    public void OnAimCancelled(InputAction.CallbackContext value)
    {
        aimInput = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition += aimInput * 1000 * Time.deltaTime;

        if (StaticValues.controller)
        {
            Mouse.current.WarpCursorPosition(mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StaticValues.controller = !StaticValues.controller;
        }
    }
}
