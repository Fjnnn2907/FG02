using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIVolume : MonoBehaviour
{
    public Slider slider;
    public string parametr;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multipliter;
    public void SiliderValue(float _value)
    {
        if (audioMixer == null || string.IsNullOrEmpty(parametr))
            return;

        float safeVal = Mathf.Clamp(_value, 0.0001f, 1f);
        float mult = multipliter > 0f ? multipliter : 20f;
        audioMixer.SetFloat(parametr, Mathf.Log10(safeVal) * mult);
    }

    public void LoadSlider(float _value)
    {
        float clamped = Mathf.Clamp(_value, 0.0001f, 1f);
        if (slider != null)
            slider.value = clamped;

        SiliderValue(clamped);
    }
}
