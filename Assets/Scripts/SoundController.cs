using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider MasterSlider;
    public Slider AmbientSlider;
    public Slider EffectsSlider;

    void Start()
    {
        MasterSlider.onValueChanged.AddListener(OnMasterSliderValueChanged);
        AmbientSlider.onValueChanged.AddListener(OnAmbientSliderValueChanged);
        EffectsSlider.onValueChanged.AddListener(OnEffectsSliderValueChanged);
    }


    public void OnMasterSliderValueChanged(float value)
    {
        audioMixer.SetFloat("Master", value);
    }
    public void OnAmbientSliderValueChanged(float value)
    {
        audioMixer.SetFloat("Ambient", value);
    }
    public void OnEffectsSliderValueChanged(float value)
    {
        audioMixer.SetFloat("Effects", value);
    }
}
