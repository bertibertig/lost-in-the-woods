using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // switches on and off state of Flashlight
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
