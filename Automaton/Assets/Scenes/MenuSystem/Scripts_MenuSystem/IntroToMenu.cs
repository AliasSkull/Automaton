using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroToMenu : MonoBehaviour
{
    private Animator mAnimator;

    public Controller input;

    public float interactButton;
    public float closeButton;
    public float dashButton;

    private void Awake()
    {
        input = new Controller();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
        input.Player.CloseMenu.performed += OnCloseMenuPerformed;
        input.Player.CloseMenu.canceled += OnCloseMenuCancelled;
        input.Player.Dash.performed += OnDashPerformed;
        input.Player.Dash.canceled += OnDashCancelled;

    }

    private void OnDisable()
    {

        input.Disable();
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
        input.Player.CloseMenu.performed -= OnCloseMenuPerformed;
        input.Player.CloseMenu.canceled -= OnCloseMenuCancelled;
        input.Player.Dash.performed -= OnDashPerformed;
        input.Player.Dash.canceled -= OnDashCancelled;
    }

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        interactButton = value.ReadValue<float>();
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {
        interactButton = value.ReadValue<float>();
    }

    public void OnCloseMenuPerformed(InputAction.CallbackContext value)
    {

        closeButton = value.ReadValue<float>();
    }

    public void OnCloseMenuCancelled(InputAction.CallbackContext value)
    {
        closeButton = value.ReadValue<float>();
    }

    public void OnDashPerformed(InputAction.CallbackContext value)
    {

        dashButton = value.ReadValue<float>();
    }

    public void OnDashCancelled(InputAction.CallbackContext value)
    {
        dashButton = value.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if(closeButton != 0 || interactButton != 0 || dashButton != 0)
            {
                mAnimator.SetTrigger("PressingSpace"); //update: this is for moving all the objects, no longer transitioning from intro to menu, intro and menu are combined into Main Menu scene. 
            }
        }
    }
}
