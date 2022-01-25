using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCamera : MonoBehaviour
{
    public void PickUp()
    {
        Destroy(this.gameObject);
        DoorOpening.closed = false;
    }
}
