using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*  ######################################################################
 *                   +DialogueTrigger : Monobehaviour
 * =======================================================================
 * + dialogueText : Text
 * + nameText : Text
 * + dialogue : Dialogue
 * =======================================================================
 * + StartDialogue() : void
 * =======================================================================
 *                                 NOTES
 * -- StartDialogue() basically passes the dialogue inside this object to
 *      the DialogueManager
 * #######################################################################
 */

[RequireComponent(typeof(AudioSource))]
public class DialogueTrigger : MonoBehaviour
{

    private AudioSource audiosource;
	public Text dialogueText; // The text to update
    public Text nameText; //The Speaker's name
    public Dialogue dialogue; //The dialogue to initiate

    private void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
    }

    //Call this method when you want to initiate the dialogue related to this trigger
    public void StartDialogue () { 
        DialogueManager.instance.StartDialogue(dialogue, audiosource, dialogueText, nameText);
    }
}
