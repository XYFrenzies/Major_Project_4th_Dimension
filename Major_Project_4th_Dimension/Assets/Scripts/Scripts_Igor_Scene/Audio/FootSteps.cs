using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioSource source;
    public SimpleAudioEvent audioEvent;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Step()
    {
        audioEvent.Play(source);
    }

}
