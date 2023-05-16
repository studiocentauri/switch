using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider[] sliders;
    private void Start()
    {
        GetVolumes();
    }
    private void GetVolumes()
    {
        var val = PlayerPrefs.GetFloat("MasterVolume", 0);
        print(val);
        mixer.SetFloat("MasterVolume", Mathf.Log10(val) * 20);
        sliders[0].value = val;
        val = PlayerPrefs.GetFloat("SFXVolume", 0);
        mixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
        sliders[1].value = val;
    }
    public void OnChaneMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    public void OnChaneSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    public void PlayLevel(int level)
    {
        SceneManage.instance.PlayLevel(level);
    }
}
