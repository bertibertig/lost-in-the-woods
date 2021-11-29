using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public double maxIterations = 100;
    [Range(0.01f,1)]
    public float velocity = 0.01f;
    [Range(0.01f, 1)]
    public float equilizeFactor = 0.99f;

    private void Start()
    {
        StartCoroutine(FloatArround());
    }

    private IEnumerator FloatArround()
    {
        Vector3 startPosition = transform.position;
        float xValue = transform.position.x;
        float zValue = transform.position.z;
        float originalVelocity = velocity;

        while (true)
        {
            for(int i = 0; i < maxIterations; i++)
            {
                float newY = transform.position.y + velocity;
                velocity *= equilizeFactor;
                transform.position = new Vector3(xValue, newY, zValue);
                yield return new WaitForSeconds(0.1f);
            }
            velocity = originalVelocity;
            velocity *= -1;
            for (int i = 0; i < maxIterations; i++)
            {
                float newY = transform.position.y + velocity;
                velocity *= equilizeFactor;
                transform.position = new Vector3(xValue, newY, zValue);
                yield return new WaitForSeconds(0.1f);
            }
            velocity = originalVelocity;
        }
    }
}
