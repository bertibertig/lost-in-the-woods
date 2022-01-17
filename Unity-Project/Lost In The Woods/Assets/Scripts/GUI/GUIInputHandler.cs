using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIInputHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject dialogSystem;
    [SerializeField]
    private GameObject interactionText;


    private bool optionsOpened = false;
    bool interactionTextActive;

    public bool OnMainMenu { get; set; }

    void Start()
    {
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
        interactionTextActive = interactionText.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && optionsMenu != null && !OnMainMenu)
        {
            if (!optionsOpened)
            {
                interactionTextActive = interactionText.activeSelf;
                Time.timeScale = 0;
                optionsMenu.SetActive(true);
                Cursor.visible = true;
                interactionText.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                optionsMenu.SetActive(false);
                Cursor.visible = false;
                if (interactionTextActive) interactionText.SetActive(true);
            }
            optionsOpened = !optionsOpened;
        }
    }
}
