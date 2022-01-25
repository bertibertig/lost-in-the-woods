using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableMindController : MonoBehaviour
{
    private GameObject player;

    public float MouseSpeed { get; set; }

    public bool CameraPickedUp { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(this);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            MouseSpeed = player.GetComponentInChildren<MouseLook>().Sensitivity;
        }

        CameraPickedUp = false;
        MouseSpeed = 5;
    }
}
