using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] MixerGroup mixerGroup;

    float currentVolume;


    public void OnToggleMute(bool isOn)
    {

        if (isOn)
        {
            // Set current volume before muting
            audioMixer.GetFloat(mixerGroup.ToString(), out currentVolume);
            audioMixer.SetFloat(this.mixerGroup.ToString(), -80);
        }
        else
        {
            audioMixer.SetFloat(this.mixerGroup.ToString(), Mathf.Log10(currentVolume) * 20);
        }

    }
}
