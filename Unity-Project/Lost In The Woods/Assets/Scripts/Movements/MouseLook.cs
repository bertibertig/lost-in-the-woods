using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotateAxis
    {
        MouseXY, MouseX, MouseY
    }

    public RotateAxis axis = RotateAxis.MouseXY;
    public float sensitivityHor = 17.0f;
    public float sensitivityVer = 17.0f;
    [Range(-180.0f, 180.0f)]
    public float minVert = -60.0f;
    [Range(-180.0f, 180.0f)]
    public float maxVert = 70.0f;

    private float rotX = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (axis == RotateAxis.MouseX)
        {
            //horizontal
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axis == RotateAxis.MouseY)
        {
            //vertical
            rotX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            rotX = Mathf.Clamp(rotX, minVert, maxVert);
            float rotY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
        else if (Cursor.lockState == CursorLockMode.Confined)
        {
            rotX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            rotX = Mathf.Clamp(rotX, minVert, maxVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}