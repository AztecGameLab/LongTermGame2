using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
    {
    public static AudioManager instance;

    // Initialized audio sources for manager
    #region
    private AudioSource musicSource;
    private AudioSource sfxSource;
    #endregion

    private void Awake()
    {
        // Making sure each audio source is assigned to an instance
        musicSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        // Loop Music
        musicSource.loop = true;

        // Assigning audio mixer child to each audio source
        AudioMixer MasterMixer = Resources.Load("Master") as AudioMixer;
        string _MixerGroup1 = "Music";
        string _MixerGroup2 = "SFX";
        musicSource.outputAudioMixerGroup = MasterMixer.FindMatchingGroups(_MixerGroup1)[0];
        sfxSource.outputAudioMixerGroup = MasterMixer.FindMatchingGroups(_MixerGroup2)[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // In case we want to set sfx clip with a volume at start
    public void PlaySFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip);
        sfxSource.volume = volume;
    }

    // In case we want to set music clip with a volume at start
    public void PlayMusic(AudioClip musicClip, float volume)
    {
        musicSource.PlayOneShot(musicClip);
        musicSource.volume = volume;
    }

    // In case we want to set music volume separately
    public void SetMusicVolume(float Volume)
    {
        musicSource.volume = Volume;
    }

    // In case we want to set sfx volume separately
    public void SetSFXVolume(float Volume)
    {
        sfxSource.volume = Volume;
    }

    // In case we want to set individual values for the min and max pitch
    public void SetSFXPitch(float pitchMin, float pitchMax)
    {
        sfxSource.pitch = Random.Range(pitchMin, pitchMax);
    }
}
