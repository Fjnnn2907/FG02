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
        audioMixer.SetFloat(parametr, Mathf.Log10(_value) * multipliter);
    }

    public void LoadSlider(float _value)
    {
        if(_value >= 0.001f)
            slider.value = _value;
    }
}
