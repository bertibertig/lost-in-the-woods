using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    public void PickUp()
    {
        Destroy(this.gameObject);
        door.GetComponent<DoorOpening>().Closed = false;
    }
}
