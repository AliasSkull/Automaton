using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //References to UI Objects
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public GameObject dialogueBox;
 

    //String queue
    private Queue<string> sentences;
    /// <summary>
    /// This bool is here to prevent any character movement while dialogue is playing. Use this bool as a condition to pause things you do not want to happen while
    /// the dialogue box is open
    /// </summary>
    public bool isDialoguePlaying;
   
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        sentences = new Queue<string>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialoguePlaying)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue) 
    {
        //This method starts the triggered dialogue from the Tutorial Manager
        //Sets dialogue box to active 
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        isDialoguePlaying = true;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        
        }
      
        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    public void EndDialogue() 
    {
        dialogueBox.SetActive(false);
        isDialoguePlaying = false;

    }

    
}
