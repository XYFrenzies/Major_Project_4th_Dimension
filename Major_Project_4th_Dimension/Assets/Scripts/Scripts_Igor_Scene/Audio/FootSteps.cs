using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioSource source;
    //public SimpleAudioEvent audioEvent;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Step()
    {
        //SoundPlayer.Instance.PlaySoundEffect("FootSteps", source);
        Debug.Log("Footstep sound");
    }

}
