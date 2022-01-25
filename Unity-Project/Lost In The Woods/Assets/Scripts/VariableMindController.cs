using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableMindController : MonoBehaviour
{
    private GameObject player;

    private float sensitivity = 5;
    private bool cameraPickedUp = false;

    public float Sensitivity
    {
        get { return sensitivity = 5; }
        set { sensitivity  = value; }
    }

    public bool CameraPickedUp
    {
        get { return cameraPickedUp; }
        set { cameraPickedUp = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponentInChildren<MouseLook>().Sensitivity = sensitivity;
        }
        
    }
}
