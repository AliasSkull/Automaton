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
    public Animator anim;
    public TutorialManager tutorialManager;
    private Dialogue activeDialogue;

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
        
        sentences = new Queue<string>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialoguePlaying)
        {
            DisplayNextSentence(activeDialogue);
        }
    }

    public void StartDialogue(Dialogue dialogue) 
    {
        //This method starts the triggered dialogue from the Tutorial Manager
        //Sets dialogue box to active 
        anim.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        isDialoguePlaying = true;
        activeDialogue = dialogue;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        
        }
      
        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(Dialogue dialogue) 
    {
        if (sentences.Count == 0)
        {
            EndDialogue(dialogue);
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

   

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue(Dialogue dialogue) 
    {
        anim.SetBool("IsOpen", false);
        isDialoguePlaying = false;
        activeDialogue.beenPlayed = true;
        activeDialogue = null;
        tutorialManager.ChangeStage();
    }

    
}
