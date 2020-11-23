using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceMgr : MonoBehaviour
{
    public AudioMixer Audiomixer;//進行控制的mixer變量

    public void SetSoundVolume(float volume)//控制Sound的函數
    {
        Audiomixer.SetFloat("SoundVolume", volume);
    }

    public void SetVoiceVolume(float volume)//控制Voice的函數
    {
        Audiomixer.SetFloat("VoiceVolume", volume);
    }

    public void BGMVolume(float volume)//控制BGM的函數
    {
        Audiomixer.SetFloat("BGMVolume", volume);
    }


}
