using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource SoundSource;//배경음 소리
    public AudioSource EffectSource;//효과음 소리

    public Slider BackGroundSoundSlider;//배경음 소리 슬라이더
    public Slider EffectSoundSlider;//이팩트 소리 슬라이더

    public List<AudioClip> SoundPlayList;//배경음 리스트
    public List<AudioClip> EffectPlayList;//효과음 리스트

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(Instance);


        if (BackGroundSoundSlider != null)
        {
            BackGroundSoundSlider.onValueChanged.AddListener(SetVolumSound);
            BackGroundSoundSlider.value = 1;
        }
        if (EffectSoundSlider != null)
        {
            EffectSoundSlider.onValueChanged.AddListener(SetVolumEffect);
            EffectSoundSlider.value = 1;
        }
            
    }

    //소리 출력
    public void PlaySound(int Index)
    {
        if (SoundPlayList != null)
        {
            SoundSource.clip = SoundPlayList[Index];
            SoundSource.Play();
        }
    }
    public void PlayEffect(int Index)
    {
        if (EffectPlayList != null)
        {
            EffectSource.clip = EffectPlayList[Index];
            EffectSource.Play();
        }
    }

    //소리 조절
    public void SetVolumSound(float value)
    {
        SoundSource.volume = value;
    }

    public void SetVolumEffect(float value)
    {
        EffectSource.volume = value;
    }
}
