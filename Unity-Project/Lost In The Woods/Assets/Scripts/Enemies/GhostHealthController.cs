using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealthController : MonoBehaviour
{
    public int maxHp = 100;

    private int hp;

    public int Hp
    {
        get { return hp; }
    }


    // spawns item
    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        hp = 0;
        hp = maxHp;
    }

    public void DamageGhost(int damageAmount)
    {
        hp -= damageAmount;
        print("Damage: " + damageAmount + "\nEnemyHP: " + hp);
        if(hp <= 0)
        {
            GetComponent<FloatToPlayer>().FollowPlayerTask.Stop();
            Instantiate(objectToSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
