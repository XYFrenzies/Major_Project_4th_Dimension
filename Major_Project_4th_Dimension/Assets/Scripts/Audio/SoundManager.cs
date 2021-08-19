using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private FMOD.Studio.EventInstance instance;

    public void Play2DSound(string soundLocation) 
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(soundLocation);
        instance.start();
        instance.release();
    }
    public void Play3DSound(string soundLocation)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(soundLocation);
        instance.start();
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    public void StopPlayingImmediately(string soundLocation) 
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(soundLocation);
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
    }
    public void StopPlayingSlowly(string soundLocation)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(soundLocation);
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }
    //This is an event that can only be modified within the FMOD software
    public void SoundInPosition(string soundLocation, Vector3 locationOfSound) 
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundLocation, locationOfSound);
    }
}
