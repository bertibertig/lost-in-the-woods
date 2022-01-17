using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToPlayer : MonoBehaviour
{
    [SerializeField]
    private float followDistance = 8.0f;
    [SerializeField]
    private float maxSpeed = 0.1f;
    [SerializeField]
    private float secondsBetweenMovement = 0.1f;
    [SerializeField]
    private bool seenPlayer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        new Task(WaitForPlayerToApproch());
        seenPlayer = false;
    }

    private IEnumerator WaitForPlayerToApproch()
    {
        float distanceToPlayer = -1;
        do
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
            yield return new WaitForEndOfFrame();
        } while (distanceToPlayer > followDistance);
        seenPlayer = true;
        new Task(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        while(seenPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, maxSpeed);
            transform.LookAt(player.transform.position);
            yield return new WaitForSeconds(secondsBetweenMovement);
        }
    }
}
