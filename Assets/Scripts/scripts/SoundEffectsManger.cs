using System;
using UnityEngine;
[Serializable]
[RequireComponent(typeof(AudioSource))]
public class SoundEffectsManger : MonoBehaviour
{
    public float minValue,maxValue;
    public float SoundSpeed;
    AudioSource audiosource;
    public void  Start()
    {
        audiosource = this.GetComponent<AudioSource>();
    }


<<<<<<< HEAD
    public void Play (AudioClip clip)
=======
    public void play (AudioClip clip,AudioSource aS)
>>>>>>> 18bbd37... AudioSource Edit
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
