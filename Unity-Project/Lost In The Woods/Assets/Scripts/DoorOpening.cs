using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private string[] dialoge;

    private bool closed = true;
    private DialogeHandler dialogeHandler;

    public bool Closed
    {
        get { return closed; }
        set { closed = value; }
    }

    private void Start()
    {
        dialogeHandler = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogeHandler>();
    }


    public void OpenDoor()
    {
        if (Closed)
        {
            dialogeHandler.StartDialog(dialoge);
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
