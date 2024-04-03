using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndScne : MonoBehaviour
{
    public Animator _an;
    public GameObject box;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBossGate()
    {
        _an.SetTrigger("Open");
        Invoke("RemoveCollisionBox", 5f);
    }

    public void RemoveCollisionBox()
    {
        box.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            SceneManager.LoadScene("EndGame");
        }
    }
}
