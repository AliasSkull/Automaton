using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AsyncSceneLoad : MonoBehaviour
{
    public string sceneName;
    public VideoClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds((float)clip.length);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
