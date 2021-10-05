using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
    public SimpleAudioEvent[] audioEvents;
    private Dictionary<string, SimpleAudioEvent> soundEvents;

    private void Awake()
    {
        soundEvents = new Dictionary<string, SimpleAudioEvent>();

        foreach (SimpleAudioEvent sae in audioEvents)
        {
            soundEvents.Add(sae.name, sae);
        }
    }

    public AudioClip GetClip(string a_AudioEvent)
    {
        if (soundEvents.TryGetValue(a_AudioEvent, out SimpleAudioEvent audioEvent))
        {
            return audioEvent.GetRandomClip;
        }

        return null;
    }

    public float GetVolume(string a_AudioEvent)
    {
        if (soundEvents.TryGetValue(a_AudioEvent, out SimpleAudioEvent audioEvent))
        {
            return audioEvent.GetVolume;
        }

        return 1f;
    }

    public float GetPitch(string a_AudioEvent)
    {
        if (soundEvents.TryGetValue(a_AudioEvent, out SimpleAudioEvent audioEvent))
        {
            return audioEvent.GetPitch;
        }

        return 1f;
    }

    public void PlaySoundEffect(string a_AudioEvent, AudioSource a_source)
    {
        if (soundEvents.TryGetValue(a_AudioEvent, out SimpleAudioEvent audioEvent))
        {
            a_source.clip = audioEvent.GetRandomClip;
            a_source.volume = audioEvent.GetVolume;
            a_source.pitch = audioEvent.GetPitch;
            a_source.Play();
        }
    }

    public void PlaySoundOnRepeat(string a_AudioEvent, AudioSource a_source)
    {
        if (soundEvents.TryGetValue(a_AudioEvent, out SimpleAudioEvent audioEvent))
        {
            a_source.clip = audioEvent.GetRandomClip;
            a_source.loop = true;
            a_source.volume = audioEvent.GetVolume;
            a_source.pitch = audioEvent.GetPitch;
            a_source.Play();
        }
    }
}
