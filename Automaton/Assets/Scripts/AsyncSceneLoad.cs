using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AsyncSceneLoad : MonoBehaviour
{
    public string sceneName;
    public VideoClip clip;

    private bool loaded;

    public Controller input;
    public float interactButt;

    private void Awake()
    {
        input = new Controller();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
        input.Player.Interact.canceled += OnInteractCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Interact.canceled -= OnInteractCancelled;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
        AudioSource _as = GetComponent<AudioSource>();
        _as.time = 5.5f;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactButt != 0 && !loaded)
        {
            StopAllCoroutines();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loaded = true;
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        interactButt = value.ReadValue<float>();
    }

    public void OnInteractCancelled(InputAction.CallbackContext value)
    {
        interactButt = value.ReadValue<float>();
    }

    public IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds((float)clip.length);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
