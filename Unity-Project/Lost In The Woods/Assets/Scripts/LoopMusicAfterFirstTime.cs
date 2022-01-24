using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMusicAfterFirstTime : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicToPlayAfter;

    private AudioSource currentAudioSource;
    private bool secondAudioStarted;
    // Start is called before the first frame update
    void Start()
    {
        currentAudioSource = GetComponent<AudioSource>();
        secondAudioStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!secondAudioStarted && !currentAudioSource.isPlaying && !musicToPlayAfter.isPlaying)
        {
            secondAudioStarted = true;
            musicToPlayAfter.Play();
            currentAudioSource.Stop();
        }
    }
}
