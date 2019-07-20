using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]

/*  ######################################################################
 *                           + DialogueSentence
 * =======================================================================
 * + Text : string
 * + afterDialogueEvent : UnityEvent
 * + HintText : string
 * + AdditionalHints : string[]
 * =======================================================================
 *                                 NOTES
 * -- HintText is the main hint to be displayed
 * -- AdditionalHints contains all of the additional hints
 * -- afterDialogueEvent is Invoked the moment Text is being written
 * #######################################################################
 */


public class DialogueSentence
{
    [TextArea(3,10)]
    public string Text;

    public UnityEvent afterDialogueEvent;

    [TextArea(1,10)]
    public string HintText;
    [TextArea(1,10)]
    public string[] AddtionalHints;
}
