using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Slider musicSlider, FXSlider;
    public AudioMixer audioMixer;
    AudioSource audioSource;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        FXSlider.onValueChanged.AddListener(OnFXSliderValueChanged);
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        musicSlider.value = GetMusicVolume();
        FXSlider.value = GetFXVolume();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("FXVolume", Mathf.Log10(volume) * 20);
    }

    public float GetMusicVolume()
    {
        float volume;
        audioMixer.GetFloat("MusicVolume", out volume);
        return Mathf.Pow(10, volume / 20);
    }

    public float GetFXVolume()
    {
        float volume;
        audioMixer.GetFloat("FXVolume", out volume);
        return Mathf.Pow(10, volume / 20);
    }

    public void OnMusicSliderValueChanged(float value)
    {
        SetMusicVolume(value);
    }

    public void OnFXSliderValueChanged(float value)
    {
        SetFXVolume(value);
    }
}
