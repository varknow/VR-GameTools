﻿using UnityEngine;

[RequireComponent(typeof(SoundEffectsManger))]
public class HumanOperator : MonoBehaviour
{

    #region Singleton
    public static HumanOperator instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //edit script begin
    public GameObject RecordIcon;
    public GameObject CorrectIcon;
    public GameObject WrongIcon;

    //ding & dong sounds
    public AudioClip wrongSound,correctSound;
    

    //edit script end
    public KeyCode CorrectAnswerKey = KeyCode.T;
    public KeyCode WrongAnswerKey = KeyCode.F;
    public KeyCode StartDialogueKey = KeyCode.R;

    private SoundEffectsManger Sfx;
    
    public VARKnowAgent varKnowAgent;

    public DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        varKnowAgent = FindObjectOfType<VARKnowAgent>();
        Sfx = GetComponent<SoundEffectsManger>();
    }

    // Update is called once per frame
    void Update()
    {
        AnswerChecking();

        if (Input.GetKeyDown(StartDialogueKey))
        {
            dialogueTrigger.StartDialogue();
        }
    }
    public void SetDialogueTrigger(DialogueTrigger trigger)
    {
        dialogueTrigger = trigger;
    }

    private void AnswerChecking()
    {
        if (Input.GetKeyDown(CorrectAnswerKey))
        {
            AnswerCommand(true);

        }

        if (Input.GetKeyDown(WrongAnswerKey))
        {
            AnswerCommand(false);
        }
    }

private void AnswerCommand(bool answer)
    {
        if (answer)
        {
            Sfx.play(wrongSound);
            DialogueManager.instance.DisplayNextSentence();
            DisplayCorrectAnswer();
        }
        else
        {
            Sfx.play(correctSound);          
            DialogueManager.instance.DisplayCurrentSentence();
            DisplayWrongAnswer();
        }

        if(varKnowAgent != null)
        {
            varKnowAgent.Disappear();
        }
        

        
        //DialogueManager.instance.DisplayNextSentence(repeat: !answer);
        //DialogueManager.instance.PerformNextEvent(repeat: !answer);
    }

    private void DisplayCorrectAnswer()
    {
        try
        {
            RecordIcon.SetActive(false);
            WrongIcon.SetActive(false);
            CorrectIcon.SetActive(true);
        }
        catch (UnassignedReferenceException)
        {
            Debug.LogWarning("Record Icon/Wrong Icon/Correct Icon not set");
        }
        
    }

    private void DisplayWrongAnswer()
    {
        try
        {
            RecordIcon.SetActive(false);
            WrongIcon.SetActive(true);
            CorrectIcon.SetActive(false);
        }
        catch (UnassignedReferenceException)
        {
            Debug.LogWarning("Record Icon/Wrong Icon/Correct Icon not set");
        }
    }

    public void DisplayRecording()
    {
        try
        {
            RecordIcon.SetActive(true);
            WrongIcon.SetActive(false);
            CorrectIcon.SetActive(false);
        }
        catch (UnassignedReferenceException)
        {
            Debug.LogWarning("Record Icon/Wrong Icon/Correct Icon not set");
        }
    }
    
    
}
