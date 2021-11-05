using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Step()
    {
        SoundPlayer.Instance.PlaySoundEffect("FootSteps", source);
    }

}
