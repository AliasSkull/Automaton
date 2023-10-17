using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneIcon : MonoBehaviour
{
    public string iconIndex;
    public bool onStart;
    
    // Start is called before the first frame update
    void Start()
    {
        if(onStart)
        gameObject.GetComponent<Image>().sprite = StaticItemIDHelper.SpriteFinder(GameObject.Find("RuneManager").GetComponent<ItemManager>().partSpriteListScriptableObject, iconIndex);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetIcon()
    {
        gameObject.GetComponent<Image>().sprite = StaticItemIDHelper.SpriteFinder(GameObject.Find("RuneManager").GetComponent<ItemManager>().partSpriteListScriptableObject, iconIndex);
    }
}
