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

public class DialogueTrigger : MonoBehaviour
{

	public Text dialogueText; // The text to update
    public Text nameText; //The Speaker's name
    public Dialogue dialogue; //The dialogue to initiate


    //Call this method when you want to initiate the dialogue related to this trigger
	public void StartDialogue () { 
        DialogueManager.instance.StartDialogue(dialogue,dialogueText,nameText);
	}
}
