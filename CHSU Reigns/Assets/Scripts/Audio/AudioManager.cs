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
    /// <summary>
    /// Воспроизвдение музыки
    /// </summary>
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

    /// <summary>
    /// Воспроизведение звуков
    /// </summary>
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
    /// <summary>
    /// Воспроизведение голоса персонажа
    /// </summary>
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
    /// <summary>
    /// Мьютим музыку (используется в UI)
    /// </summary>
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    /// <summary>
    /// Мьютим звуки (используется в UI)
    /// </summary>
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        voiceSource.mute = !voiceSource.mute;
    }
    /// <summary>
    /// Изменяем громкость музыки (используется в UI)
    /// </summary>
    public void MusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
    /// <summary>
    /// Изменяем громкость звуков (используется в UI)
    /// </summary>
    public void SFXVolume()
    {
        sfxSource.volume = sfxSlider.value;
        voiceSource.volume = sfxSlider.value;
    }
}
