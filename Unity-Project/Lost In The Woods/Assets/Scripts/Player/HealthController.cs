using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private Animator playerHealthGUIAnimator;
    [SerializeField]
    private int maxHealth = 3;
    [SerializeField]
    private int playerMaxBackwardsRotationX = -90;
    [SerializeField]
    private bool killPlayerOnZeroHealth = true;
    [SerializeField]
    private TextMeshProUGUI deathText;
    [SerializeField]
    private string sceneToLoadAfterDeath;
    [SerializeField]
    private AudioSource[] damageSounds;
    [SerializeField]
    private AudioSource deathSound;
    [SerializeField]
    private AudioSource ekgFlatSound;

    private GameObject player;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ApplyDamage(int damage)
    {
        if(CurrentHealth - damage <= 0)
        {
            CurrentHealth = 0;
            KillPlayer();
        }
        else
        {
            CurrentHealth -= damage;
            if (damageSounds != null)
            {
                damageSounds[Random.Range(0, damageSounds.Length)].Play();
            }
        }

        if (playerHealthGUIAnimator != null)
        {
            playerHealthGUIAnimator.SetInteger("Damage", CurrentHealth);
        }
    }

    private void KillPlayer()
    {
        if(killPlayerOnZeroHealth)
        {
            deathSound.Play();
            ekgFlatSound.Play();

            player.GetComponent<PlayerMovementController>().FreezePlayer();

            deathText.enabled = true;
            deathText.alpha = 1;
            deathText.text = "You are dead! Game Over!";

            new Task(RotatePlayerBackwards());
        }
    }

    private IEnumerator RotatePlayerBackwards()
    {
        float xPos = 0;
        do
        {
            player.transform.Rotate(-0.5f, 0, 0);
            xPos += -0.5f;
            yield return new WaitForEndOfFrame();
        } while (xPos > playerMaxBackwardsRotationX);

        Task.WaitForSecondsTask(5).Finished += HealthController_Finished;

        
    }

    private void HealthController_Finished(bool manual)
    {
        SceneManager.LoadScene(sceneToLoadAfterDeath);
    }
}
