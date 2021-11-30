using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] footprintSound;
    [SerializeField] private float footstepDuration = 1;

    void Start()
    {
        StartCoroutine(Step());
    }

    void Update()
    {
        //if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !soundSource.isPlaying)
        //{
        //    soundSource.clip = footprintSound[Random.Range(0, footprintSound.Length)];
        //    soundSource.Play(0);
        //}
    }

    IEnumerator Step()
    {
        while (true)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                soundSource.PlayOneShot(footprintSound[Random.Range(0, footprintSound.Length)]);
                yield return new WaitForSeconds(footstepDuration);
            }
            yield return null;
        }
    }
}
