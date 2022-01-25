using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    public void LookAtPlayer(float additionalX = 0, float additionalY = 0, float additionalZ = 0)
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        transform.Rotate(additionalX, additionalY, additionalZ);
    }
}
