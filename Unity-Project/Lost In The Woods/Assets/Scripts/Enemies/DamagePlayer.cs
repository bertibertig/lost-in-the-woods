using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private int damageOnContact = 1;
    
    private bool damageAllowed = true;
    private GameObject player;
    private HealthController healthController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        healthController = player.GetComponent<HealthController>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player") && damageAllowed)
        {
            healthController.ApplyDamage(damageOnContact);
        }
    }
}
