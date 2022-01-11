using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealthController : MonoBehaviour
{
    public int maxHp = 100;

    private int hp = 0;

    // spawns item
    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    public void DamageGhost(int damageAmount)
    {
        hp -= damageAmount;
        print("Damage: " + damageAmount + "\nEnemyHP: " + hp);
        if(hp <= 0)
        {
            Destroy(gameObject);
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}
