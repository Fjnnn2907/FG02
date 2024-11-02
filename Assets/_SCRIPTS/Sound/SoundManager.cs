using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] musicSounds, fxSounds;
    public AudioSource musicSource, fxSource;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
            Debug.Log("Not Sound Music");
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        Sound s = Array.Find(fxSounds, x => x.name == clip.name);
        if(s == null)
            Debug.Log("Not Sound Effect");
        else
        {
            fxSource.clip = s.clip;
            fxSource.Play();
        }
    }

    public void MuteMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void MuteFX()
    {
        fxSource.mute = !fxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void FXVolume(float volume)
    {
        fxSource.volume = volume;
    }
}
