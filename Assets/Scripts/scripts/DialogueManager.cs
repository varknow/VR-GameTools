using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/*  ######################################################################
 *                  + DialogueManager : Monobehaviour
 * =======================================================================
 *  - instance : DialogueManager
 *  - nameText : Text
 *  - dialogueText : Text
 *  - sentences : Queue<DialogueSentence>
 *  - isTyping : Bool = false
 *  - currentDialogueSentence : DialogueSentence
 *  - currentDialogueEvent : UnityEvent
 *  - endOfDialogueEvents : UnityEvent
 *  # soundClip : AudioClip
 *  =======================================================================
 *  - Start() : void
 *  - SetTextPlaceHolder(dialogueText:Text , nameText:Text) : void
 *  + StartDialogue(dialogue:Dialogue) : void
 *  - PerformCurrentEvent() : void
 *  + DisplayNextSentence() : void
 *  + DisplayCurrentSentence() : void
 *  - TypeSentences(sentence:string) : IEnumerator
 *  - EndDialogue() : void
 *  + FinishedSoundEffect(audio:AudioClip) : IEnumerator
 *  =======================================================================
 *                                  NOTES
 *  -- DialogueManager is a SINGLETON and 
 *      ONLY ONE OF IT MUST EXIST IN THE SCENE
 *  -- To start a dialogue, a Dialogue object is required.
 *      (Usually set by DialogueTrigger)
 *  -- Progressing the dialogue is possible using
 *      DisplayNextSentence and DisplayCurrentSentence functions
 *  ######################################################################  
 */

public class DialogueManager : MonoBehaviour {

    #region Singleton
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private Text nameText;
    private Text dialogueText;

    private Queue<DialogueSentence> sentences;

    private bool isTyping = false;

    private DialogueSentence currentDialogueSentence;
    private UnityEvent currentDialogueEvent;

    private UnityEvent endOfDialogueEvents;

    internal AudioClip soundClip;
    public AudioClip audioclip;

    public SoundEffectsManger Sfx;


    void Start () {
        sentences = new Queue<DialogueSentence>();
        instance = this;
	}


    //Used internally to update the dialogue text and name placeholders
    private void SetTextPlaceHolder(Text dialogueText, Text nameText)
    {
        try
        {
            this.dialogueText = dialogueText;
        } catch (NullReferenceException)
        {
            Debug.LogError("Dialogue Text has not been set. Where do you want to show your dialogue???");
        }
        if (nameText != null) this.nameText = nameText;
    }


    //called by DialogueTrigger
    public void StartDialogue(Dialogue dialogue, Text dialogueText = null, Text nameText = null)
    {
        
        Debug.Log("Starting Conversation with " + dialogue.name);

        //Set where to show the dialogue
        SetTextPlaceHolder(dialogueText, nameText);

        //Update the Name
        nameText.text = dialogue.name;

        //Clear and fill the sentences queue
        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //Update the EndOfDialogueEvents
        if (dialogue.EndOfDialogueEvents != null)
        {
            this.endOfDialogueEvents = dialogue.EndOfDialogueEvents;
        }

        //Start Typing
        if (!isTyping)
        {
            DisplayNextSentence();
        }
    }

    //Performs the current dequeued event
    private void PerformCurrentEvent()
    {
        if (currentDialogueEvent != null)
        {
            currentDialogueEvent.Invoke();
        }
    }

    //Pop the sentence queue
    public void DisplayNextSentence()
    {
        //Check if empty
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Pop the queue
        currentDialogueSentence = sentences.Dequeue();

        //Update Values
        AudioClip clip = currentDialogueSentence.audioclip;
        currentDialogueEvent = currentDialogueSentence.afterDialogueEvent;
        HintSystem.instance.SetHint(currentDialogueSentence.HintText);
        HintSystem.instance.SetAdditionalHintArray(currentDialogueSentence.AddtionalHints);

        //Type the popped sentence and invoke it's event
        StartCoroutine(TypeSentences(currentDialogueSentence.Text));
        PerformCurrentEvent();
        //PlayDialogueSound
        Sfx.play(clip);
        

    }

    //Displays the current dequeued sentence
    public void DisplayCurrentSentence()
    {
        StartCoroutine(TypeSentences(currentDialogueSentence.Text));
        PerformCurrentEvent();
    }

    //Coroutine for displaying sentences.
    private IEnumerator TypeSentences (string sentence)
    {
        if (!isTyping)
        {
            isTyping = true;
            dialogueText.text = "";
            foreach (char letter in sentence)
            {
                //display each letter with 0.01 second delay
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.01f);
            }

            //AfterSoundEffect();
            isTyping = false;
            HumanOperator.instance.DisplayRecording();
        }
    }

    //Performed after there are no more sentences.
    private void EndDialogue()
    {
        //Invoke the event
        endOfDialogueEvents.Invoke();

        //Clear the DialogueTrigger
        HumanOperator.instance.dialogueTrigger = null;
    }

    //Shows recording UI after the Clip Length
    public IEnumerator FinishedSoundEffect(AudioClip audio)
    {
        yield return new WaitForSeconds(audio.length);
        HumanOperator.instance.DisplayRecording();
    }


}
