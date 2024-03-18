using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AsyncSceneLoad : MonoBehaviour
{
    public string sceneName;
    public VideoClip clip;

    private bool loaded;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !loaded)
        {
            StopAllCoroutines();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loaded = true;
        }
    }

    public IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds((float)clip.length);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
