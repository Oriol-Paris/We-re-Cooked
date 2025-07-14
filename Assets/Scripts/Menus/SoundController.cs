using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private void Start()
    {
        float master = PlayerPrefs.GetFloat("Master", 1f);
        float music = PlayerPrefs.GetFloat("Music", 1f);
        float sfx = PlayerPrefs.GetFloat("SFX", 1f);
        float ambient = PlayerPrefs.GetFloat("Ambient", 1f);

        audioMixer.SetFloat("Master", Mathf.Log10(master) * 20);
        audioMixer.SetFloat("Music", Mathf.Log10(music) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfx) * 20);
        audioMixer.SetFloat("Ambient", Mathf.Log10(ambient) * 20);
    }

    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        SaveVolume("Master",sliderValue);
    }

    public void SetAmbienceVolume(float sliderValue)
    {
        audioMixer.SetFloat("Ambience", Mathf.Log10(sliderValue) * 20);
        SaveVolume("Ambience",sliderValue);
    }
    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        SaveVolume("Music",sliderValue);
    }
    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        SaveVolume("SFX",sliderValue);
    }

    void SaveVolume(string mixer, float value)
    {
        if (value <= 0) value = 0.001f;
        PlayerPrefs.SetFloat(mixer, value);
    }
}
