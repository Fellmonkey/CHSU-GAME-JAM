using System;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds, voiceSounds;
    public AudioSource musicSource, sfxSource, voiceSource;
    public Slider musicSlider, sfxSlider;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("reigns");
    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.nameSound == name);

        if(sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();

        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.nameSound == name);

        if(sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
    public void PlayVoice(string name)
    {
        Sound sound = Array.Find(voiceSounds, x => x.nameSound == name);

        if(sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            voiceSource.PlayOneShot(sound.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
    public void SFXVolume()
    {
        sfxSource.volume = sfxSlider.value;
        voiceSource.volume = sfxSlider.value;
    }
}
