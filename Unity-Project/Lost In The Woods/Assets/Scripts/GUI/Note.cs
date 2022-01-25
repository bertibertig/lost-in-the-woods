using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private GameObject noteGUIObject;
    [SerializeField]
    private string noteText;

    private TextMeshProUGUI noteMeshText;
    private Image noteImage;
    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        noteImage = noteGUIObject.GetComponent<Image>();
        noteMeshText = noteGUIObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OpenNode()
    {
        interactable.DisableInteraction();
        noteMeshText.text = noteText;
        
        noteImage.enabled = true;
        noteMeshText.enabled = true;
        new Task(WaitForInteractPress());
    }

    private IEnumerator WaitForInteractPress()
    {
        bool buttonPressed = false;
        while (!buttonPressed)
        {
            if(Input.GetButtonDown("Interact"))
            {
                buttonPressed = true;
            }
            yield return null;
        }
        interactable.EnableInteraction();
        noteImage.enabled = false;
        noteMeshText.enabled = false;
    }
}
