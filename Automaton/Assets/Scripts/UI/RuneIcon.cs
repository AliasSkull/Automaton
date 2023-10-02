using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneIcon : MonoBehaviour
{
    public int iconIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = StaticItemIDHelper.SpriteFinder(GameObject.Find("ItemManager").GetComponent<ItemManager>().partSpriteListScriptableObject, iconIndex.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
