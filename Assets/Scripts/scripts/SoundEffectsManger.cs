using System;
using UnityEngine;
[Serializable]
[RequireComponent(typeof(AudioSource))]
public class SoundEffectsManger : MonoBehaviour
{
    #region Singleton
    public static SoundEffectsManger instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public float minValue,maxValue;
    public float SoundSpeed;
    public AudioSource audioSource;
    public void  Start()
    {
        //audiosource = this.GetComponent<AudioSource>();
    }


    public void Play (AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        
    }

    public void Stop(string n)
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

}
