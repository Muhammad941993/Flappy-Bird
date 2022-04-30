using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager
{ 
    public enum Sound { Jumb , Score , Lose , ButtonOver , ButtonClicked }


    static public void PlaySound(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        gameObject.tag = "Sound";
    }

    static private AudioClip GetAudioClip(Sound sound)
    {

        foreach (Assets.SoundAudioClip clip in Assets.GetInstance().soundAudioClipsArr)
        {
            if (clip.sound == sound)
            {
                return clip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found");
        return null;
    }


}
