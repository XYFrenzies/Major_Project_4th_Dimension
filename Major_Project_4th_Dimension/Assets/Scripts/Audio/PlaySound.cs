using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void StartConveyorSoundEffect()
    {
        SoundPlayer.Instance.PlaySoundEffect("Big Switch Flip", source);
    }
}
