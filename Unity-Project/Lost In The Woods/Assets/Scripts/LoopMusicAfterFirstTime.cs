using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMusicAfterFirstTime : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicToPlayAfter;

    private AudioSource currentAudioSource;
    private bool secondAudioStarted;
    private bool continueAudio;
    // Start is called before the first frame update
    void Start()
    {
        currentAudioSource = GetComponent<AudioSource>();
        secondAudioStarted = false;
        continueAudio = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!secondAudioStarted && !currentAudioSource.isPlaying && !musicToPlayAfter.isPlaying && continueAudio)
        {
            secondAudioStarted = true;
            musicToPlayAfter.Play();
            currentAudioSource.Stop();
        }
    }

    public void StopMusic()
    {
        continueAudio = false;
        musicToPlayAfter.Stop();
        currentAudioSource.Stop();
    }
}
