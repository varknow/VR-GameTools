using UnityEngine.Events;

/*  ######################################################################
 *                               + Dialogue
 * =======================================================================
 * + name : string
 * + sentences : DialogueSentence[]
 * + EndOfDialogueEvents : UnityEvent
 * =======================================================================
 *                                 NOTES
 * -- DialogueSentence[] is an array in which each index contains data
 *      for each sentence to be displayed.
 * -- This Object is included within DialogueTrigger
 * #######################################################################
 */

[System.Serializable]
public class Dialogue {

    //Speaker's Name
    public string name;
  
    //Data related to each sentence
    public DialogueSentence[] sentences;

    //Events to Invoke At the end of the Dialogue
    public UnityEvent EndOfDialogueEvents;
}
