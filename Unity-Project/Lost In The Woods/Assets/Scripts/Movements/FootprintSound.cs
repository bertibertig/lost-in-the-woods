using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] footprintSound;
    [SerializeField] private float footstepDurationWalk = 0.75f;
    [SerializeField] private float footstepDurationRun = 0.3f;

    void Start()
    {
        StartCoroutine(Step());
    }

    IEnumerator Step()
    {
        while (true)
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>().DisableMovement)
            {
                soundSource.PlayOneShot(footprintSound[Random.Range(0, footprintSound.Length)]);
                if (GetComponent<PlayerMovementController>().isNormalSpeed())
                {
                    yield return new WaitForSeconds(footstepDurationWalk);
                }
                else
                {
                    yield return new WaitForSeconds(footstepDurationRun);
                }
            }
            yield return null;
        }
    }
}
