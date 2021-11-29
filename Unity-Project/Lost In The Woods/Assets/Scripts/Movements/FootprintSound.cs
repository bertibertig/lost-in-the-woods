using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] footprintSound;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 /* || cc.isGrounded() */)
        {
            soundSource.clip = footprintSound[Random.Range(0, footprintSound.Length)];
            soundSource.Play(0);
        }
    }
}
