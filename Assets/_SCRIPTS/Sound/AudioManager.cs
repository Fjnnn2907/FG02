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
    private bool wasPlayingBGM;
    private Dictionary<AudioSource, float> defaultSfxVolumes = new Dictionary<AudioSource, float>();
    private Dictionary<AudioSource, Coroutine> fadeCoroutines = new Dictionary<AudioSource, Coroutine>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (sfx != null)
        {
            for (int i = 0; i < sfx.Length; i++)
            {
                if (sfx[i] != null && !defaultSfxVolumes.ContainsKey(sfx[i]))
                    defaultSfxVolumes[sfx[i]] = sfx[i].volume;
            }
        }

        Invoke("MuteSFX", 1f);
    }

    private void Update()
    {
        if (!playBGM)
        {
            if (wasPlayingBGM)
            {
                StopBGM();
                wasPlayingBGM = false;
            }
        }
        else if (bgm != null && bgm.Length > 0)
        {
            wasPlayingBGM = true;
            bgmIndex = Mathf.Clamp(bgmIndex, 0, bgm.Length - 1);
            if (bgm[bgmIndex] != null && !bgm[bgmIndex].isPlaying)
                PlayBGM(bgmIndex);
        }
    }

    public void PlayRamdomBGM()
    {
        if (bgm == null || bgm.Length == 0)
            return;

        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlaySFX(int _index, Transform _sound)
    {
        if (!canPlaySFX || sfx == null || _index < 0 || _index >= sfx.Length || sfx[_index] == null)
            return;

        if (_sound != null && PlayerManager.instance != null && PlayerManager.instance.character != null)
        {
            if (Vector2.Distance(PlayerManager.instance.character.transform.position, _sound.position) > distanceSound)
                return;
        }

        if (defaultSfxVolumes.TryGetValue(sfx[_index], out float baseVol))
            sfx[_index].volume = baseVol;

        sfx[_index].pitch = Random.Range(.85f, 1.1f);
        sfx[_index].Play();
    }
    public void StopSFX(int _index)
    {
        if (sfx == null || _index < 0 || _index >= sfx.Length || sfx[_index] == null)
            return;

        sfx[_index].Stop();
    }


    public void StopSFXWithTime(int _index)
    {
        if (sfx == null || _index < 0 || _index >= sfx.Length || sfx[_index] == null)
            return;

        AudioSource src = sfx[_index];
        if (!defaultSfxVolumes.ContainsKey(src))
            defaultSfxVolumes[src] = src.volume;

        if (fadeCoroutines.TryGetValue(src, out Coroutine running) && running != null)
            StopCoroutine(running);

        fadeCoroutines[src] = StartCoroutine(SFXToEnd(src));
    }

    IEnumerator SFXToEnd(AudioSource _audio)
    {
        if (_audio == null)
            yield break;

        float deflautVolume = defaultSfxVolumes.TryGetValue(_audio, out float saved) ? saved : _audio.volume;

        while (_audio != null && _audio.volume > 0.1f)
        {
            _audio.volume -= _audio.volume * .2f;
            yield return new WaitForSecondsRealtime(.25f);
        }

        if (_audio != null)
        {
            _audio.Stop();
            _audio.volume = deflautVolume;
            fadeCoroutines.Remove(_audio);
        }
    }
    public void PlayBGM(int _index)
    {
        if (bgm == null || _index < 0 || _index >= bgm.Length || bgm[_index] == null)
            return;

        bgmIndex = _index;

        StopBGM();
        bgm[bgmIndex].Play();
        wasPlayingBGM = true;
    }

    private void StopBGM()
    {
        if (bgm == null)
            return;

        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i] != null)
                bgm[i].Stop();
        }
    }

    private void MuteSFX() => canPlaySFX = true;
}
