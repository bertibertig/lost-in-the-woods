using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualRoomOutro : MonoBehaviour
{
    [SerializeField]
    private string[] outroStartText;
    [SerializeField]
    private string[] conversationText;
    [SerializeField]
    private GameObject lookTowardsGameObject;
    [SerializeField]
    private AudioSource gunShotSound;
    [SerializeField]
    private LoopMusicAfterFirstTime music;

    private DialogeHandler dialogeHandler;
    private GameObject player;
    private bool outroStarted;

    private void Start()
    {
        dialogeHandler = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogeHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
        outroStarted = false;
    }

    public void StartOutro()
    {
        GetComponent<Interactable>().DisableInteraction();
        if(outroStartText != null && !outroStarted)
        {
            outroStarted = true;
            dialogeHandler.StartDialog(outroStartText);
            if (lookTowardsGameObject != null)
            {
                dialogeHandler.FinishedDialog += SpawnWomenBehindPlayerAndStartConversation;
            }
        }
    }

    private void SpawnWomenBehindPlayerAndStartConversation()
    {
        player.transform.LookAt(lookTowardsGameObject.transform);
        dialogeHandler.StartDialog(conversationText);

        dialogeHandler.FinishedDialog -= SpawnWomenBehindPlayerAndStartConversation;
        dialogeHandler.FinishedDialog += ShootPlayer;
    }

    private void ShootPlayer()
    {
        music.StopMusic();
        gunShotSound.Play();
        player.GetComponent<PlayerMovementController>().FreezePlayer();

        dialogeHandler.FinishedDialog -= SpawnWomenBehindPlayerAndStartConversation;
        Task.WaitForSecondsTask(2).Finished += RitualRoomOutro_Finished;
    }

    private void RitualRoomOutro_Finished(bool manual)
    {
        player.GetComponent<HealthController>().KillPlayer("You were shot! The game is over.", "Office", true);
    }
}
