using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource loopAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;

    public void ToggleSfx()
    {
        sfxAudioSource.enabled = !sfxAudioSource.enabled;
        loopAudioSource.enabled = !loopAudioSource.enabled;
    }

    public void ToggleMusic()
    {
        musicAudioSource.enabled = !musicAudioSource.enabled;
    }

    public void PlaySfx(AudioClip audioClip, bool loop = false)
    {
        if (!loop)
        {
            if (!sfxAudioSource.enabled) return;

            sfxAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            if (!loopAudioSource.enabled) return;
            if (loopAudioSource.isPlaying) return;

            loopAudioSource.clip = audioClip;
            loopAudioSource.loop = true;
            loopAudioSource.Play();
        }
    }

    public void StopSfx(AudioClip audioClip)
    {
        if (sfxAudioSource.enabled &&
            sfxAudioSource.isPlaying &&
            sfxAudioSource.clip == audioClip)
        {
            sfxAudioSource.Stop();
        }
        else if (loopAudioSource.enabled &&
                 loopAudioSource.isPlaying &&
                 loopAudioSource.clip == audioClip)
        {
            loopAudioSource.Stop();
        }
    }
}