using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisDoorSound : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayOasisDoorSound()
    {
        SoundPlayer.Instance.PlaySoundEffect("OasisDoor", source);
    }
}
