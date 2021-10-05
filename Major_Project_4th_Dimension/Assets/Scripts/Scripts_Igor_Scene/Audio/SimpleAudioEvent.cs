using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Events/Simple")]
public class SimpleAudioEvent : ScriptableObject
{
    public AudioClip[] clips;

    public RangedFloat volumeRange;
    float vol;

    [MinMaxRange(0, 2)]
    public RangedFloat pitchRange;
    float pitch;

    public AudioClip GetRandomClip
    {
        get
        {
            return clips[Random.Range(0, clips.Length)];
        }
    }

    public float GetVolume
    {
        get
        {
            return vol = Random.Range(volumeRange.minValue, volumeRange.maxValue);
        }
    }

    public float GetPitch
    {
        get
        {
            return pitch = Random.Range(pitchRange.minValue, pitchRange.maxValue);
        }
    }

}