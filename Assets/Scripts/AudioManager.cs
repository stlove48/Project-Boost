using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager amInstance;


    private void Awake()
    {
        if (amInstance != null && amInstance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            amInstance = this;
        }

        DontDestroyOnLoad(this);

    }

}
