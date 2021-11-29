using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static float GRAVITY = 9.8f;

    private float speed = 10.0f;
    private float currGravity = 0;

    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation != new Quaternion(0,0,0,1))
        {
            
        }

        print(transform.rotation);

        float dx = Input.GetAxis("Horizontal") * speed;
        float dz = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(dx * Time.deltaTime, 0, dz * Time.deltaTime);

        currGravity -= GRAVITY * Time.deltaTime;

        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);
        movement = new Vector3(movement.x, currGravity, movement.z);

        cc.Move(movement);
    }
}