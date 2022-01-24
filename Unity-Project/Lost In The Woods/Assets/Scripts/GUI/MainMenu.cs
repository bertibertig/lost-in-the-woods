using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //variables for fadeOut of Main Menu
    private int numberOfFadeOutTasks = 0;
    private List<Task> fadeOutTasks = new List<Task>();

    [SerializeField]
    private GameObject startCutscene;

    [SerializeField]
    private bool showMainMenu;

    [SerializeField]
    private GameObject[] uiElementsToDisable;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (showMainMenu)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            SetUIElementsActive(false);
            if(player != null)
            {
                player.GetComponent<PlayerMovementController>().FreezePlayer();
                player.GetComponentInChildren<Flashlight>().FlashLightDisabled = true;
            }
            Cursor.visible = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetUIElementsActive(bool activeStatus)
    {
        foreach(GameObject go in uiElementsToDisable)
        {
            if (go != null)
            {
                go.SetActive(activeStatus);
            }
        }
    }

    public void OnNewGameButton()
    {
        FadeOutMainMenu();
    }

    private void BeginGame()
    {
        SetUIElementsActive(true);
        player.GetComponent<PlayerMovementController>().UnfreezePlayer();
        Cursor.visible = false;
        startCutscene.GetComponent<OfficeBeginningCutscene>().StartCutscene();
        transform.gameObject.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    private void FadeOutMainMenu()
    {
        foreach(Transform tf in transform)
        {
            if(tf.gameObject.GetComponent<TextMeshProUGUI>() != null)
            {
                fadeOutTasks.Add(new Task(FadeOutText(tf.gameObject.GetComponent<TextMeshProUGUI>()), false));
            }
            else if(tf.gameObject.GetComponent<Button>() != null)
            {
                fadeOutTasks.Add(new Task(FadeOutImage(tf.gameObject.GetComponent<Image>()), false));
                fadeOutTasks.Add(new Task(FadeOutText(tf.gameObject.GetComponentInChildren<TextMeshProUGUI>()), false));
            }
            else
            {
                Debug.LogError("New Gameobject Type in Main Menu Fade Out found and not handled");
            }
        }

        //start FadeOutTasks

        numberOfFadeOutTasks = 0;
        foreach (Task t in fadeOutTasks)
        {
            t.Finished += T_Finished;
            t.Start();
        }
    }

    private void T_Finished(bool manual)
    {
        numberOfFadeOutTasks++;

        if(numberOfFadeOutTasks == fadeOutTasks.Count)
        {
            BeginGame();
        }
    }

    private IEnumerator FadeOutImage(Image image, float fadeOutSpeed = 0.01f)
    {
        do
        {
            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                image.color.a - 0.01f);
            yield return new WaitForSeconds(fadeOutSpeed);
        } while (image.color.a > 0);
        
    }

    private IEnumerator FadeOutText(TextMeshProUGUI text, float fadeOutSpeed = 0.01f)
    {
        do
        {
            text.color = new Color(
                text.color.r,
                text.color.g,
                text.color.b,
                text.color.a - 0.01f);
            yield return new WaitForSeconds(fadeOutSpeed);
        } while (text.color.a > 0);
        yield return new WaitForSeconds(1);
    }
}
