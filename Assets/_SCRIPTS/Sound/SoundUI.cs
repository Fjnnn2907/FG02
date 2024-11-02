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
        soundManager = SoundManager.instance;
    }
    public void MuteMusic()
    {
        soundManager.MuteMusic();
    }
    public void MuteFX()
    {
        soundManager.MuteFX();
    }
    public void MusicVolume()
    {
        soundManager.MusicVolume(musicSound.value);
    }
    public void FXVolume()
    {
        soundManager.FXVolume(musicFX.value);
    }
}
