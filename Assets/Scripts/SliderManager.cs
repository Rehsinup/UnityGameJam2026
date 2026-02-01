using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(value) * 20f);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MyExposedParam 2", Mathf.Log10(value) * 20f);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("MyExposedParam 1", Mathf.Log10(value) * 20f);
    }
}
