using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string displayText = "Press E";
    public string sceneNameToLoad;
    public TextMeshProUGUI interactionText;

    public bool PlayerLookingAtObject { get; set; }

    [SerializeField]
    private bool exitEnabled = false;
    [SerializeField]
    private string[] textIfExitIsDisabled;
    [SerializeField]
    private UnityEvent methodsToExecuteAfterDialogHasEnded;

    private DialogeHandler dialogeHandler;

    public bool DialogEnded { get; set; }

    private void Start()
    {
        PlayerLookingAtObject = false;
        interactionText.enabled = false;
        DialogEnded = true;
        dialogeHandler = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogeHandler>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && PlayerLookingAtObject)
        {
            if (exitEnabled)
            {
                if (Application.CanStreamedLevelBeLoaded(sceneNameToLoad))
                {
                    SceneManager.LoadScene(sceneNameToLoad);
                }
                else
                {
                    print("Scene: " + sceneNameToLoad + " does not exist.");
                }
            }
            else if(DialogEnded)
            {
                if(dialogeHandler != null && textIfExitIsDisabled != null && textIfExitIsDisabled.Length > 0)
                {
                    DialogEnded = false;
                    dialogeHandler.StartDialog(textIfExitIsDisabled, methodsToExecuteAfterDialogHasEnded);
                }
                else
                {
                    Debug.LogError("Dialog Handler not found");
                }
            }
        }
    }

    public void LookingAtObject()
    {
        PlayerLookingAtObject = true;
        interactionText.text = displayText;
        interactionText.enabled = true;
    }

    public void LookingAway()
    {
        PlayerLookingAtObject = false;
        interactionText.text = "";
        interactionText.enabled = false;
    }

    public void EnableExit()
    {
        exitEnabled = true;
    }

    public void DialogFinished()
    {
        new Task(DialogWait());
    }

    private IEnumerator DialogWait()
    {
        yield return new WaitForSeconds(0.5f);
        DialogEnded = true;
    }
}
