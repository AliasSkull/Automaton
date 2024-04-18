using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenuScreen;

    public Controller input;

    public bool paused;
    public bool pressed;

    public float interactButt;
    public float dashButt;
    public bool reset;

    private void Awake()
    {
        input = new Controller();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Pause.performed += OnPausePerformed;
        input.Player.Pause.canceled += OnPauseCancelled;

        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;

        input.Player.Dash.performed += OnDashPerformed;
        input.Player.Dash.canceled += OnDashCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Pause.performed -= OnPausePerformed;
        input.Player.Pause.canceled -= OnPauseCancelled;
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;

        input.Player.Dash.performed -= OnDashPerformed;
        input.Player.Dash.canceled -= OnDashCancelled;
    }

    public void OnPausePerformed(InputAction.CallbackContext value)
    {
        if (!paused && !pressed)
        {
            Time.timeScale = 0;
            pauseMenuScreen.SetActive(true);

            pressed = true;
            paused = true;
        }
        else if (paused && !pressed)
        {
            Time.timeScale = 1;
            pauseMenuScreen.SetActive(false);

            pressed = true;
            paused = false;
        }
    }

    public void OnPauseCancelled(InputAction.CallbackContext value)
    {
        pressed = false;
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        interactButt = value.ReadValue<float>();
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {
        interactButt = 0;
    }

    public void OnDashPerformed(InputAction.CallbackContext value)
    {
        dashButt = value.ReadValue<float>();
    }

    public void OnDashCancelled(InputAction.CallbackContext value)
    {
        dashButt = 0;
    }

    public void Update()
    {
        if (paused && dashButt != 0 && interactButt != 0 && !reset)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("IntroOpening");
            reset = true;
        }
    }
}
