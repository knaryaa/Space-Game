using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sounds;
    
    public void PlayCrystalCollect()
    {
        sounds[0].Play();
    }

    public void PlayBackground()
    {
        sounds[1].Play();
    }
}
