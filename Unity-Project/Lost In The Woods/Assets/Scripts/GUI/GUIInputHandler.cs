using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInputHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject dialogSystem;
    [SerializeField]
    private GameObject interactionText;
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private GameObject cameraUI;
    [SerializeField]
    private Image[] ghostBalls;
    
    private bool optionsOpened = false;
    bool interactionTextActive;
    bool cameraOpened;

    public bool OnMainMenu { get; set; }

    public bool CameraPickedUp { get; set; }

    public bool CameraDisplayed { get; set; }

    public Image[] GhostBalls
    {
        get { return ghostBalls; }
        private set { ghostBalls = value; }
    }


    void Start()
    {
        interactionTextActive = interactionText.activeSelf;
        cameraOpened = cameraUI.activeSelf;
        if (GameObject.FindGameObjectWithTag("VariableMind") != null)
        {
            CameraPickedUp = GameObject.FindGameObjectWithTag("VariableMind").GetComponent<VariableMindController>().CameraPickedUp;
        }
        else
        {
            CameraPickedUp = true;
        }
        CameraDisplayed = false;
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
                cameraUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                optionsMenu.SetActive(false);
                Cursor.visible = false;
                if (interactionTextActive) interactionText.SetActive(true);
                if (cameraOpened) cameraUI.SetActive(true);
            }
            optionsOpened = !optionsOpened;
        }

        if(Input.GetButtonDown("Camera") && cameraUI != null && !OnMainMenu && CameraPickedUp)
        {
            if(!CameraDisplayed)
            {
                cameraUI.SetActive(true);
                crosshair.SetActive(false);
                CameraDisplayed = true;
            }
            else
            {
                cameraUI.SetActive(false);
                crosshair.SetActive(true);
                CameraDisplayed = false;
            }
        }
    }
}
