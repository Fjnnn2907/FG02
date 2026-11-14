using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public Slider musicSound, musicFX;

    public SoundManager soundManager;

    private void Start()
    {
        if (soundManager == null)
            soundManager = SoundManager.instance;
    }
    public void MuteMusic()
    {
        if (soundManager != null)
            soundManager.MuteMusic();
    }
    public void MuteFX()
    {
        if (soundManager != null)
            soundManager.MuteFX();
    }
    public void MusicVolume()
    {
        if (soundManager != null && musicSound != null)
            soundManager.MusicVolume(musicSound.value);
    }
    public void FXVolume()
    {
        if (soundManager != null && musicFX != null)
            soundManager.FXVolume(musicFX.value);
    }
}
