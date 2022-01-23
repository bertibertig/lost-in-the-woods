using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    private TextMeshProUGUI textToFadeOut;

    private float textAlphaFull = 1;

    private void Start()
    {
        textToFadeOut = GetComponent<TextMeshProUGUI>();
    }

    public Task FadeOut(float durationInSeconds, bool autoStart = true)
    {
        return new Task(FadeOutTextCo(durationInSeconds), autoStart);
    }

    private IEnumerator FadeOutTextCo(float duartion)
    {       
        float fade = (textAlphaFull / (duartion * 60));
        do
        {
            textToFadeOut.alpha -= fade;
            yield return new WaitForEndOfFrame();
        } while (textToFadeOut.alpha > 0);

        textToFadeOut.enabled = false;
        textToFadeOut.alpha = 1;
    }

    public Task FadeIn(float durationInSeconds, string? text = null, bool autoStart = true)
    {
        if (text != null)
        {
            textToFadeOut.text = text;
        }
        return new Task(FadeInTextCo(durationInSeconds), autoStart);
    }

    private IEnumerator FadeInTextCo(float duartion)
    {
        float fade = (textAlphaFull / (duartion * 60));
        textToFadeOut.alpha = 0;
        textToFadeOut.enabled = true;
        
        do
        {
            textToFadeOut.alpha += fade;
            yield return new WaitForEndOfFrame();
        } while (textToFadeOut.alpha < 1);
    }
}
