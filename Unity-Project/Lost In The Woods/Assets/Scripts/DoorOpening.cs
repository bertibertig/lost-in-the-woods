using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [HideInInspector]
    public static bool closed = true;

    public void OpenDoor()
    {
        if (closed)
        {
            // add text to display
            GameObject.Find("ChangeVisability").GetComponent<Visibilty>().EnableVisibility();
        }
        else
        {
            StartCoroutine(RotateDoor());
            door.GetComponent<Interactable>().DisableInteraction();
        }
    }
    private IEnumerator RotateDoor()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);
            this.transform.Rotate(0, 4.5f, 0);
        }
    }
}
