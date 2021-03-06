using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;

    public float minY = -60f;
    public float maxY = 60f;

    private GameObject player;
    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // md is mosue delta
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        // the interpolated float result between the two float values
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        // incrementally add to the camera look
        mouseLook += smoothV;

        // vector3.right means the x-axis
        RestrictMouse();
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }

    void RestrictMouse()
    {
        if (mouseLook.y >= maxY)
            mouseLook.y = 60;
        if (mouseLook.y <= minY)
            mouseLook.y = -60;
    }
}
