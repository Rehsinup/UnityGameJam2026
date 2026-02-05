using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Audio/Sound FX", fileName ="Sound FX")]
public class SOSound : ScriptableObject
{
    public string id;
    public AudioClip[] clips;
    public Vector2 volume; //min/max volume
    public Vector2 pitch; //min/max pitch

    public void Play(AudioSource source)
    {
        source.pitch = Random.Range(pitch.x, pitch.y);

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        source.PlayOneShot(clip, Random.Range(volume.x, volume.y));
    }
}
