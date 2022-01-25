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

    public void KillPlayer(string deathText = "You are dead! Game Over!", string sceneToLoadAfterDeath = null, bool fallDown = false)
    {
        if(sceneToLoadAfterDeath != null)
        {
            this.sceneToLoadAfterDeath = sceneToLoadAfterDeath;
        }
        if(killPlayerOnZeroHealth)
        {
            playerHealthGUIAnimator.SetInteger("Damage", 0);
            deathSound.Play();
            ekgFlatSound.Play();

            player.GetComponent<PlayerMovementController>().FreezePlayer();

            this.deathText.enabled = true;
            this.deathText.alpha = 1;
            this.deathText.text = deathText;

            if (!fallDown)
            {
                new Task(RotatePlayerBackwards());
            }
            else
            {
                new Task(FallDown());
            }
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

    private IEnumerator FallDown()
    {
        float yPos = player.transform.position.y;
        float newyPos = player.transform.position.y - 1.2f;

        float downSpeed = 0.1f;
        do
        {
            this.transform.position += Vector3.down * Time.deltaTime * downSpeed;
            yield return new WaitForEndOfFrame();
        } while (this.transform.position.y > newyPos);

        Task.WaitForSecondsTask(5).Finished += HealthController_Finished;
    }
}
