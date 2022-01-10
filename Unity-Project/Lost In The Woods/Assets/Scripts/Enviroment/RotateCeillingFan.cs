using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCeillingFan : MonoBehaviour
{
    [Range(0.1f, 5),SerializeField]
    private float rotationSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
}
