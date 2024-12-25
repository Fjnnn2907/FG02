using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBGM;
    public int bgmIndex;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if(!playBGM)
            StopBGM();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
                PlayBGM(bgmIndex);
        }
    }

    public void PlayRamdomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlaySFX(int _index)
    {
        if(_index < sfx.Length)
        {
            sfx[_index].pitch = Random.Range(.85f,1.1f);
            sfx[_index].Play();
        }
    }
    public void StopSFX(int _index)
    {
        sfx [_index].Stop();
    }
    
    public void PlayBGM(int _index)
    {
        bgmIndex = _index;

        StopBGM();
        bgm[bgmIndex].Play();
    }

    private void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
