using UnityEngine;

public class AudioCollision : MonoBehaviour
{
    AudioSource source;
    public SimpleAudioEvent audioEvent;
    public bool alreadyHit = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyHit)
        {
            return;
        }
        else
        { 
            audioEvent.Play(source);
            alreadyHit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (alreadyHit)
        {
            alreadyHit = false;
        }
    }
}
