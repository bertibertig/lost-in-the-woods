using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OfficeBeginningCutscene : MonoBehaviour
{
    [SerializeField]
    private GameObject telephone;
    [SerializeField]
    private GameObject telephoneHandset;
    [SerializeField]
    private DialogeHandler dialogHandler;
    [SerializeField]
    private UnityEvent methodsToExecuteAfterDialogeEnd;

    [SerializeField]
    private string[] dialog;

    [SerializeField]
    private string[] dialogAfterTelephoneConversation;

    //Telephone Sounds
    [SerializeField]
    private AudioSource ringTelephoneAudio;
    [SerializeField]
    private AudioSource pickUpTelephoneAudio;
    [SerializeField]
    private AudioSource hangUpTelephoneAudio;

    private GameObject player;
    private Interactable interactable;
    private Vector3 initialPositionOfHandset;
    private Quaternion initialRotationOfHandset;
    private LevelExit levelExit;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactable = telephone.GetComponent<Interactable>();
        levelExit = GameObject.FindGameObjectWithTag("LevelExit").GetComponent<LevelExit>();
    }

    public void StartCutscene()
    {  
        interactable.DiableInteraction();
        var waitTesk = new Task(Task.WaitForSeconds(1));
        waitTesk.Finished += RingTelephone;
    }

    private void RingTelephone(bool manual)
    {
        if(ringTelephoneAudio != null)
        {
            ringTelephoneAudio.Play();
            interactable.EnableInteraction();
        }
        else
        {
            Debug.LogError("Telephone Game Object is null.");
        }
    }

    public void OnInteraction(GameObject sender)
    {
        ringTelephoneAudio.Stop();
        pickUpTelephoneAudio.Play();
        
        if(sender != null && sender.CompareTag("Interactable"))
        {
            sender.GetComponent<Interactable>().DiableInteraction();
            sender.GetComponent<Interactable>().LookingAway();
            StartTelephoneConversation();
        }
        else
        {
            Debug.LogError("sender is empty or not of type interactable");
        }
    }

    private void MoveHandsetToPlayer()
    {
        //TODO
        initialPositionOfHandset = telephoneHandset.transform.position;
        initialRotationOfHandset = telephoneHandset.transform.rotation;
    }

    private void StartTelephoneConversation()
    {
        MoveHandsetToPlayer();
        dialogHandler.StartDialog(dialog, methodsToExecuteAfterDialogeEnd);
        dialogHandler.FinishedDialog += DialogHandler_FinishedDialog;
    }

    private void DialogHandler_FinishedDialog()
    {
        hangUpTelephoneAudio.Play();
        dialogHandler.FinishedDialog -= DialogHandler_FinishedDialog;
    }

    public void AfterTelephoneConversation()
    {
        if(dialogAfterTelephoneConversation != null && levelExit != null)
        {
            levelExit.TextIfExitIsDisabled = dialogAfterTelephoneConversation;
        }
        else
        {
            levelExit.EnableExit();
        }
    }
}
