using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 3;
    [SerializeField]
    private bool killPlayerOnZeroHealth = true;
    [SerializeField]
    private AudioSource[] damageSounds;
    [SerializeField]
    private AudioSource deathSound;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
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
    }

    private void KillPlayer()
    {
        if(killPlayerOnZeroHealth)
        {
            deathSound.Play();
            print("You are dead! Game Over!");
        }
    }
}
