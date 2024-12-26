using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private float distanceSound;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBGM;
    public int bgmIndex;

    private bool canPlaySFX;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        Invoke("MuteSFX", 1f);
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

    public void PlaySFX(int _index,Transform _sound)
    {
        //if(bgm[bgmIndex].isPlaying)
        //    return;

        if(!canPlaySFX)
            return;

        if (_sound != null && Vector2.Distance(PlayerManager.instance.character.transform.position, _sound.transform.position) > distanceSound)
            return;

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
    

    public void StopSFXWithTime(int _index)
    {
        StartCoroutine(SFXToEnd(sfx[_index]));
    }

    IEnumerator SFXToEnd(AudioSource _audio)
    {
        float deflautVolume = _audio.volume;

        while(_audio.volume > 0.1f)
        {
            _audio.volume -= _audio.volume * .2f;
            yield return new WaitForSeconds(.25f);

            if(_audio.volume <= .1f)
            {
                _audio.Stop();
                _audio.volume = deflautVolume;
                break;
            }

        }
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

    private void MuteSFX() => canPlaySFX = true;
}
