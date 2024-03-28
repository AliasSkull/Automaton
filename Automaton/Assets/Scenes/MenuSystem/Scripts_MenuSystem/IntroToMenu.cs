using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToMenu : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                mAnimator.SetTrigger("PressingSpace"); //update: this is for moving all the objects, no longer transitioning from intro to menu, intro and menu are combined into Main Menu scene. 
            }
        }
    }
}
