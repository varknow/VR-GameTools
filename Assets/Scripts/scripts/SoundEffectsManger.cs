using System;
using UnityEngine;
[Serializable]
[RequireComponent(typeof(AudioSource))]
public class SoundEffectsManger : MonoBehaviour
{
    //public SoundEffect[] Effcets;
    AudioSource audiosource;
    public void  Start()
    {
        audiosource = this.GetComponent<AudioSource>();
    }


    public void Play (AudioClip clip)
    {
        audiosource.clip = clip;
        audiosource.Play();
    }

    public void Stop(string n)
    {
        audiosource.Stop();
        audiosource.clip = null;
    }

}
