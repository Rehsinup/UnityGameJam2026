using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] AudioSource[] musicPlayers;
    [SerializeField] AudioSource sfxPlayer;

    [Header("Mixer")]
    [SerializeField] AudioMixer mixer;
    [SerializeField] float defaultVolume;


    [Header("Media")]
    [SerializeField] AudioClip[] musics;
    [SerializeField] List<SOSound> sounds;

    int currentPlayer = -1;
    Dictionary<string, SOSound> dicoSound;

    // Start is called before the first frame update
    void Start()
    {
        dicoSound = new Dictionary<string, SOSound>();
        mixer.SetFloat("MusicVolume", defaultVolume);
        
        foreach(SOSound sfx in sounds)
        {
            dicoSound.Add(sfx.id, sfx);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer == -1) return;

        if (musicPlayers[currentPlayer].volume < 1)
        {
            int previous = (currentPlayer + musicPlayers.Length - 1) % musicPlayers.Length;
            float vol = Mathf.Min(1, musicPlayers[currentPlayer].volume + Time.deltaTime);
            musicPlayers[currentPlayer].volume = vol;
            musicPlayers[previous].volume = 1 - vol;

            if (vol == 1f)
            {
                musicPlayers[previous].Stop();
            }
        }

    }

    public void PlayMusic(int index)
    {
        AudioClip music = musics[index];

        currentPlayer = (currentPlayer + 1) % musicPlayers.Length;

        musicPlayers[currentPlayer].clip = music;
        musicPlayers[currentPlayer].volume = 0;
        musicPlayers[currentPlayer].Play();

    }

    public void PlaySound(string id)
    {
        PlaySound(id, sfxPlayer);
    }

    public void PlaySound(string id, AudioSource source)
    {
        if (dicoSound.ContainsKey(id))
        {
            SOSound sound = dicoSound[id];
            sound.Play(source);
        }
    }

    public void SetSnapshot(AudioMixerSnapshot snap)
    {
        snap.TransitionTo(0.3f);
    }
}
