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


    public void play (AudioClip clip)
    {
        audiosource.clip = clip;
        audiosource.Play();
    }

    public void stop(string n)
    {
        audiosource.Stop();
        audiosource.clip = null;
    }

    internal void play(int index)
    {
        throw new NotImplementedException();
    }

}
