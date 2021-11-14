using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10.0f;

    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed;
        float dz = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(dx * Time.deltaTime, 0, dz * Time.deltaTime);

        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);
        movement = new Vector3(movement.x, 0, movement.z);

        cc.Move(movement);

        print(movement);
    }
}