using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectsMixer;

    public AudioSource MissSword, BackGroundMusic;

    public static AudioManager instance;

    [Range(-80, 10)]
    public float masterVol, effectsVol;

    public Slider masterSlider, effectSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(BackGroundMusic);
        masterSlider.value = masterVol;
        effectSlider.value = effectsVol;
        BackGroundMusic.loop = true;

        masterSlider.minValue = -80;
        masterSlider.maxValue = 10;
        effectSlider.minValue = -80;
        effectSlider.maxValue = 10;
    }

    // Update is called once per frame
    void Update()
    {
        MasterVolume();
        EffectVolume();
        
    }

    public void MasterVolume()
    {
        musicMixer.SetFloat("masterVolume", masterSlider.value);
    }

    public void EffectVolume()
    {
        effectsMixer.SetFloat("effectVolume", effectSlider.value);
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}