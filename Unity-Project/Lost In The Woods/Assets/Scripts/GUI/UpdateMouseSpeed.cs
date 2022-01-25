using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMouseSpeed : MonoBehaviour
{
    public float maxValue = 20;

    private GameObject player;
    private MouseLook lookScript;
    private Scrollbar scrollbar;
    private TextMeshProUGUI speedText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            lookScript = player.GetComponent<MouseLook>();
            lookScript.Sensitivity = 5;
        }
        scrollbar = GetComponentInParent<Scrollbar>();
        speedText = GetComponent<TextMeshProUGUI>();

        if(speedText != null && scrollbar != null && lookScript != null)
        {
            speedText.text = lookScript.Sensitivity.ToString();
            scrollbar.value = lookScript.Sensitivity / maxValue;
        }
        else
        {
            print("Mouse Speed could not be set");
        }
    }

    public void OnValueChanged()
    {
        if (lookScript != null && scrollbar != null && speedText != null)
        {
            if(scrollbar.value == 0)
            {
                scrollbar.value = 0.01f;
            }

            double mouseSpeed = Math.Round(scrollbar.value, 2) * maxValue;
            speedText.text = mouseSpeed.ToString();

            lookScript.Sensitivity = (float) mouseSpeed;
            if (GameObject.FindGameObjectWithTag("VariableMind") != null)
            {
                GameObject.FindGameObjectWithTag("VariableMind").GetComponent<VariableMindController>().Sensitivity = (float)mouseSpeed;
            }
        }
    }
}
