using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIInputHandler : MonoBehaviour
{

    public GameObject optionsMenu;

    private bool optionsOpened = false;

    void Start()
    {
        if(optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && optionsMenu != null)
        {
            if(!optionsOpened)
            {
                Time.timeScale = 0;
                optionsMenu.SetActive(true);
                Cursor.visible = true; 
            }
            else
            {
                Time.timeScale = 1;
                optionsMenu.SetActive(false);
                Cursor.visible = false;
            }
            optionsOpened = !optionsOpened;
        }
    }
}
