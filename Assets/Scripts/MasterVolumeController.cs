using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
public class MasterVolumeController : MonoBehaviour
{
    AudioSource source;
    float originalMusicVolume;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        originalMusicVolume = source.volume;
    }

    void Update()
    {
        AudioListener.volume = RTConsole.Singleton.GetConVar<float>("master_vol").value;
        source.volume = originalMusicVolume * RTConsole.Singleton.GetConVar<float>("music_vol").value;
    }

    public void SetMaster(float master)
    {
        RTConsole.Singleton.GetConVar<float>("master_vol").value = master;
    }

    public void SetMusic(float music)
    {
        RTConsole.Singleton.GetConVar<float>("music_vol").value = music;
    }
}
