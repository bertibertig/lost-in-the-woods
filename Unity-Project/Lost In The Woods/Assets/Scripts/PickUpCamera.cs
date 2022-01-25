using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationText;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private string cameraUsageText = "Press Q to toggle camera.";
    [SerializeField]
    private float fadeInOutSeconds = 3;
    [SerializeField]
    private float displayForSeconds = 5;

    [SerializeField]
    private string[] dialoge;
    private DialogeHandler dialogeHandler;

    private FadeText fadeText;
    private GUIInputHandler gUIInputHandler;
    private VariableMindController variableMindController;

    private void Start()
    {
        dialogeHandler = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogeHandler>();

        if (notificationText != null)
        {
            fadeText = notificationText.GetComponent<FadeText>();
        }

        gUIInputHandler = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIInputHandler>();
        gUIInputHandler.CameraPickedUp = false;

        if(GameObject.FindGameObjectWithTag("VariableMind") != null)
        {
            variableMindController = GameObject.FindGameObjectWithTag("VariableMind").GetComponent<VariableMindController>();
        }
    }

    public void PickUp()
    {
        dialogeHandler.StartDialog(dialoge);
        //dialogeHandler.FinishedDialog += DialogeHandler_FinishedDialog;

        door.GetComponent<DoorOpening>().Closed = false;
        GetComponentInChildren<Interactable>().DisableInteraction();
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;

        gUIInputHandler.CameraPickedUp = true;
        if(variableMindController != null)
        {
            variableMindController.CameraPickedUp = true;
        }

        fadeText.FadeIn(fadeInOutSeconds, cameraUsageText).Finished += FadeOutTask_Finished;
    }

    private void DialogeHandler_FinishedDialog()
    {
        throw new System.NotImplementedException();
    }

    //Wait for seconds and let the player read the text
    private void FadeOutTask_Finished(bool manual)
    {
        Task.WaitForSecondsTask(displayForSeconds).Finished += PickUpCamera_Finished;
    }

    private void PickUpCamera_Finished(bool manual)
    {
        fadeText.FadeOut(fadeInOutSeconds).Finished += PickUpCamera_Finished1;
    }

    private void PickUpCamera_Finished1(bool manual)
    {
        Destroy(gameObject);
    }


}
