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
    [Range(-180.0f, 180.0f)]
    public float minVert = -60.0f;
    [Range(-180.0f, 180.0f)]
    public float maxVert = 70.0f;

    private float rotX = 0.0f;

    [SerializeField]
    private float sensitivity = 5;

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    private static float DEFAULT_SENSITIVITY = 5;

    private void Start()
    {
        GameObject variableMind = GameObject.FindGameObjectWithTag("VariableMind");

        if(variableMind != null)
        {
            sensitivity = variableMind.GetComponent<VariableMindController>().Sensitivity;
        }
        else
        {
            sensitivity = DEFAULT_SENSITIVITY;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (axis == RotateAxis.MouseX)
        {
            //horizontal
            transform.Rotate(0, Input.GetAxis("Mouse X") * Sensitivity, 0);
        }
        else if (axis == RotateAxis.MouseY)
        {
            //vertical
            rotX -= Input.GetAxis("Mouse Y") * Sensitivity;
            rotX = Mathf.Clamp(rotX, minVert, maxVert);
            float rotY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
        else if (axis == RotateAxis.MouseXY)
        {
            rotX -= Input.GetAxis("Mouse Y") * Sensitivity;
            rotX = Mathf.Clamp(rotX, minVert, maxVert);
            float delta = Input.GetAxis("Mouse X") * Sensitivity;
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