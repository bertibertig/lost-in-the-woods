using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string displayText = "Press E";
    public string sceneNameToLoad;
    public TextMeshProUGUI interactionText;

    private bool lookingAtObject = false;

    private void Start()
    {
        interactionText.enabled = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && lookingAtObject)
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
    }

    public void LookingAtObject()
    {
        lookingAtObject = true;
        interactionText.text = displayText;
        interactionText.enabled = true;
    }

    public void LookingAway()
    {
        lookingAtObject = false;
        interactionText.text = "";
        interactionText.enabled = false;
    }
}
