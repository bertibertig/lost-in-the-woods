using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpFlashlight : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationText;
    [SerializeField]
    private string flashlightUsageText = "Press F to toggle flashlight.";
    [SerializeField]
    private float fadeInOutSeconds = 3;
    [SerializeField]
    private float displayForSeconds = 5;

    private GameObject player;
    private FadeText fadeText;
    private bool interactedOnce;
    private Flashlight flashlightScript;
    private LevelExit levelExit;
    // Start is called before the first frame update
    void Start()
    {
        interactedOnce = false;
        player = GameObject.FindGameObjectWithTag("Player");
        flashlightScript = player.GetComponentInChildren<Flashlight>();
        flashlightScript.enabled = false;
        levelExit = GameObject.FindGameObjectWithTag("LevelExit").GetComponent<LevelExit>();

        if (notificationText != null)
        {
            fadeText = notificationText.GetComponent<FadeText>();
        }
    }

    public void PickedUpFlashlight()
    {
        if (!interactedOnce)
        {
            interactedOnce = true;

            GetComponent<Interactable>().DiableInteraction();
            GetComponent<MeshRenderer>().enabled = false;
            levelExit.EnableExit();

            fadeText.FadeIn(fadeInOutSeconds, flashlightUsageText).Finished += FadeOutTask_Finished;
            flashlightScript.enabled = true;
        }
    }

    //Wait for seconds and let the player read the text
    private void FadeOutTask_Finished(bool manual)
    {
        Task.WaitForSecondsTask(displayForSeconds).Finished += PickUpFlashlight_Finished;
    }

    private void PickUpFlashlight_Finished(bool manual)
    {
        fadeText.FadeOut(fadeInOutSeconds).Finished += PickUpFlashlight_Finished1;
    }

    private void PickUpFlashlight_Finished1(bool manual)
    {
        Destroy(gameObject);
    }
}
