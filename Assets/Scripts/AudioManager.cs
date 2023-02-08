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
    }

    void Start() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)  {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
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
