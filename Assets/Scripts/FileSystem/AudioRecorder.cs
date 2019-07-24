using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordingNew;
    private AudioClip recording;
    private float startRecordingTime;
    private string filename = "Clip";
    int clipNumber = 0;

    //Keep this one as a global variable (outside the functions) too and use GetComponent during start to save resources
     AudioSource audioSource;

    //Get the audiosource here to save resources
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        //TODO: Human Operator declares recording
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            EndRecording();
        }
    }

    //Start Recording the voice of Player 
    public void StartRecording()
    {
        //Get the max frequency of a microphone, if it's less than 44100 record at the max frequency, else record at 44100
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
        if (maxFreq < 44100)
            freq = maxFreq;

        //Start the recording, the length of 300 gives it a cap of 5 minutes
        recording = Microphone.Start("", false, 300, 44100);
        //Microphone won't record more than 5 minutes of audio if EndREcording is not called;

        //the time at the beginning of this frame
        startRecordingTime = Time.time;

    }


    public void EndRecording()
    {
        //End the Recording the voice of Player 
        Microphone.End("");

        //Trim the audioclip by the length of the recording
        recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
        float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
        recording.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.recording = recordingNew;


        //Play recording
        audioSource.clip = recording;

        //Save recording
        //SavWav.Save(filename, audioSource.clip);

        SaveSystem.SaveVoiceClip(audioSource.clip,clipNumber);
        clipNumber++;

    }


}
