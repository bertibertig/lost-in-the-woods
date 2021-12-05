using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private static float GRAVITY = 9.8f;

    public float normalSpeed = 5.0f;
    public float runningSpeed = 10.0f;

    private float currGravity = 0f;
    private float currSpeed = 0f;

    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        currSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Run"))
        {
            currSpeed = runningSpeed;
        }
        else
        {
            currSpeed = normalSpeed;
        }

        float dx = Input.GetAxis("Horizontal") * currSpeed;
        float dz = Input.GetAxis("Vertical") * currSpeed;

        Vector3 movement = new Vector3(dx * Time.deltaTime, 0, dz * Time.deltaTime);

        currGravity -= GRAVITY * Time.deltaTime;

        movement = Vector3.ClampMagnitude(movement, currSpeed);
        movement = transform.TransformDirection(movement);
        movement = new Vector3(movement.x, currGravity, movement.z);

        cc.Move(movement);
    }
}