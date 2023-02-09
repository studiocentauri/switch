using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] List<AudioMixerGroup> audioMixers;
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

            if(!s.isSFX) {
                s.source.outputAudioMixerGroup = audioMixers[0];
            }
            else {
                s.source.outputAudioMixerGroup = audioMixers[1];
            }
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


    public void ChangeMasterVolume() {

    }

    public void ChangeSFXVolume() {
        
    }
}
