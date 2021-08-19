using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a playerSound for when the player does anything within the scene.
/// </summary>
public class PlayerSounds : SoundManager
{
    [SerializeField] private string footsteps = "";
    [SerializeField] private string playerSoundEffectsMoving = "";
    [SerializeField] private string playerSoundEffectsStopping = "";
    [SerializeField] private List<string> audioLines;
    public void PlayFootSteps()
    {
        Play2DSound(footsteps);
    }
    public void StopFootSteps()
    {
        StopPlayingImmediately(footsteps);
    }
    public void PlaySoundEffectsWhileMoving()
    {
        Play2DSound(playerSoundEffectsMoving);
    }
    public void StopSoundEffectsWhileMoving()
    {
        Play2DSound(playerSoundEffectsStopping);
    }
    public void PlayRandomAudioLines()
    {
        int min = 0;
        int max = audioLines.Count - 1;
        if (max != 0)
        {
            int value = Random.Range(min, max);
            Play3DSound(audioLines[value]);
        }
    }
    public void PlaySpecificAudioLine(string audioLineDirectory) 
    {
        for (int i = 0; i < audioLines.Count; i++)
        {
            if (audioLines[i] == audioLineDirectory)
            {
                Play3DSound(audioLineDirectory);
            }
        }
    }
}
