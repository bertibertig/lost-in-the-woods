using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    private void Start()
    {
        openDoor();
    }
    public void openDoor()
    {
        this.transform.Rotate(0, 90, 0);
    }
}
