using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Assets : MonoBehaviour
{
    static private Assets instance;

    private void Awake()
    {
        instance = this;
    }
    static public Assets GetInstance()
    {
        return instance;
    }
    public enum PlayerState { WaitingToStart,Playing, Died }

    public Transform PfPipeHead;
    public Transform PfPipeBody;
    public Score score;
    public Bird Bird;
    public Level level;
  
    public SoundAudioClip[] soundAudioClipsArr;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

}


