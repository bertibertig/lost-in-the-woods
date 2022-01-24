using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPromt;

    [SerializeField]
    private float maxWait = 150;

    [SerializeField]
    private string sceneNameToLoad;

    private int counter;
    private float addToAlpha;
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        addToAlpha = (textPromt.alpha / maxWait) + 0.001f;
        textPromt.alpha = 0;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            counter++;
            textPromt.alpha += addToAlpha;
        }
        else
        {
            counter = 0;
            textPromt.alpha = 0;
        }

        if(counter == maxWait)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
