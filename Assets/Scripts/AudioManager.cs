using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] sounds; 
    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        foreach (Sound s in sounds) {
            s.source.Play();
        }
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        // Debug.Log(s.source.clip.name);
        s.source.Play();
    }

    // public void ChangeMasterVolume(float value) {
    //     AudioListener.volume = value;
    // }

    // public void ToggleEffect() {
    //     _effectsSource.mute = !_effectsSource.mute;
    // }

    // public void ToggleMusic() {
    //     _musicSource.mute = !_musicSource.mute;
    // }
}
