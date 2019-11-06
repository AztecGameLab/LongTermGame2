using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Byron
{
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

        public void PlaySFX(AudioClip sfxClip)
        {
            sfxSource.PlayOneShot(sfxClip);
        }

        public void PlayMusic(AudioClip musicClip)
        {
            musicSource.PlayOneShot(musicClip);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
