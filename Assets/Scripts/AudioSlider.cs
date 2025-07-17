using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public enum MixerGroup { MasterVolume, BGMVolume, SFXVolume}

public class AudioSlider : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TextMeshProUGUI volumeText;
    [SerializeField] MixerGroup mixerGroup;

    float currentVolume;

    public void OnChangeSlider(float sliderValue)
    {
        volumeText.text = $"{(sliderValue * 100).ToString("N1")}";

        audioMixer.SetFloat(mixerGroup.ToString(), Mathf.Log10(sliderValue) * 20);
        currentVolume = sliderValue;
    }

    
}
