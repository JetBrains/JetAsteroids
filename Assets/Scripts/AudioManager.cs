using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource loopAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    public void PlaySfx(AudioClip audioClip, bool loop = false)
    {
        if (!loop)
        {
            sfxAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            if (loopAudioSource.isPlaying) return;

            loopAudioSource.clip = audioClip;
            loopAudioSource.loop = true;
            loopAudioSource.Play();
        }
    }

    public void StopSfx(AudioClip audioClip)
    {
        if (sfxAudioSource.isPlaying && sfxAudioSource.clip == audioClip)
        {
            sfxAudioSource.Stop();
        }
        else if (loopAudioSource.isPlaying && loopAudioSource.clip == audioClip)
        {
            loopAudioSource.Stop();
        }
    }
}