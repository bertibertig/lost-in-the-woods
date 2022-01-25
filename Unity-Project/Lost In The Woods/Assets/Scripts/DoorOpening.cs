using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public GameObject door;
   public void openDoor()
    {
        this.transform.Rotate(0, 90, 0);
        door.GetComponent<Interactable>().DisableInteraction();
    }
}
