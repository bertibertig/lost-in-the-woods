using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogeHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogeText;

    [SerializeField]
    private Image dialogeBox;

    [SerializeField]
    private TextMeshProUGUI interactButton;

    private int currentDialogCounter;
    private bool dialogStarted;
    private string[] dialog;
    private GameObject player;
    private UnityEvent methodsToExecuteAfterDialogeEnd;

    public delegate void DialogFinishedHandler();
    public event DialogFinishedHandler FinishedDialog;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogeBox.enabled = false;
        dialogeText.enabled = false;
        interactButton.enabled = false;
        dialogStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && dialog != null && dialog.Length > 0)
        {
            if (currentDialogCounter < dialog.Length)
            {
                PrintNextDialog();
                currentDialogCounter++;
            }
            else if(currentDialogCounter == dialog.Length)
            {
                HideDialogUI();
                player.GetComponent<PlayerMovementController>().UnfreezePlayer();
                if (methodsToExecuteAfterDialogeEnd != null)
                {
                    methodsToExecuteAfterDialogeEnd.Invoke();
                }
                FinishedDialog.Invoke();
            }
        }

    }

    public void StartDialog(string[] dialog, UnityEvent methodsToExecuteAfterDialogeEnd = null)
    {
        player.GetComponent<PlayerMovementController>().FreezePlayer();
        dialogeBox.enabled = true;
        dialogeText.enabled = true;
        interactButton.enabled = true;
        currentDialogCounter = 0;
        dialogStarted = true;
        this.dialog = dialog;
        this.methodsToExecuteAfterDialogeEnd = methodsToExecuteAfterDialogeEnd;
        PrintNextDialog();
        currentDialogCounter++;
    }

    private void PrintNextDialog()
    {
        dialogeText.text = dialog[currentDialogCounter];
    }

    private void HideDialogUI()
    {
        dialogeBox.enabled = false;
        dialogeText.enabled = false;
        interactButton.enabled = false;
    }
}
