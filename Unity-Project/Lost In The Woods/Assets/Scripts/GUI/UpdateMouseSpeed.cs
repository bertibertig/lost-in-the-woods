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
        }
        scrollbar = GetComponentInParent<Scrollbar>();
        speedText = GetComponent<TextMeshProUGUI>();

        if(speedText != null && scrollbar != null && lookScript != null)
        {
            speedText.text = lookScript.SensitivityHor.ToString();
            scrollbar.value = lookScript.SensitivityHor / maxValue;
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

            lookScript.SensitivityHor = (float) mouseSpeed;
            lookScript.SensitivityVer = (float) mouseSpeed;
        }
    }
}
