using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    AudioSource source;
    public bool playMusic;
    public string MusicToPlay;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (playMusic && !source.isPlaying)
            PlaySong(MusicToPlay);

        if (source.isPlaying && !playMusic)
            source.Stop();
    }

    public void PlaySong(string musicTitle)
    {
        SoundPlayer.Instance.PlaySoundOnRepeat(musicTitle, source);
    }
}
