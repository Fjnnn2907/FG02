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
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        if (musicSounds == null || musicSource == null)
            return;

        Sound s = Array.Find(musicSounds, x => x != null && x.name == name);
        if (s == null || s.clip == null)
            Debug.Log("Not Sound Music");
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        if (fxSounds == null || fxSource == null)
            return;

        Sound s = Array.Find(fxSounds, x => x != null && x.name == name);
        if (s == null || s.clip == null)
            Debug.Log("Not Sound Effect");
        else
        {
            fxSource.PlayOneShot(s.clip);
        }
    }

    public void MuteMusic()
    {
        if (musicSource != null)
            musicSource.mute = !musicSource.mute;
    }
    public void MuteFX()
    {
        if (fxSource != null)
            fxSource.mute = !fxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;
    }
    public void FXVolume(float volume)
    {
        if (fxSource != null)
            fxSource.volume = volume;
    }
}
