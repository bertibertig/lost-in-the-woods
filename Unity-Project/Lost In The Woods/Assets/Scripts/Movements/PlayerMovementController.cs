using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private static float GRAVITY = 9.8f;

    public float normalSpeed = 2.0f;
    public float runningSpeed = 6.0f;

    private float currGravity = 0f;
    private float currSpeed = 0f;

    public float maxStamina = 15f;
    [SerializeField] private float currStamina;
    private float staminaRegenTime = 1.5f;

    private CharacterController cc;

    [SerializeField]
    private AudioSource playerOutOfBreath;
    private bool outOfStaminaSoundPlayed;

    public bool DisableMovement { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        currSpeed = normalSpeed;
        DisableMovement = false;
        currStamina = maxStamina;
        outOfStaminaSoundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DisableMovement)
        {
            if (Input.GetButton("Run"))
            {
                currStamina = Mathf.Clamp(currStamina - (Time.deltaTime), 0.0f, maxStamina);
                staminaRegenTime = 0f;

                if (!outOfStaminaSoundPlayed && currStamina <= 1)
                {
                    outOfStaminaSoundPlayed = true;
                    playerOutOfBreath.Play();
                }
            }
            else if (currStamina < maxStamina)
            {
                if (staminaRegenTime >= 1.5f)
                {
                    currStamina = Mathf.Clamp(currStamina + (Time.deltaTime) * 3, 0.0f, maxStamina);
                    outOfStaminaSoundPlayed = false;
                }
                else
                {
                    staminaRegenTime += Time.deltaTime;
                }
            }

            if (currStamina == 0 || staminaRegenTime > 0)
            {
                currSpeed = normalSpeed;
            } 
            else
            {
                currSpeed = runningSpeed;
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

    public void FreezePlayer()
    {
        DisableMovement = true;
        GameObject gui = GameObject.FindGameObjectWithTag("GUI");
        GetComponentInChildren<Raycaster>().DisableRaycaster();
        MouseLook mouseLook = gameObject.GetComponent<MouseLook>();
        Flashlight flashlight = GetComponentInChildren<Flashlight>();
        if (gui != null)
        {
            GUIInputHandler guiInputHandler = gui.GetComponent<GUIInputHandler>();
            if (guiInputHandler != null)
            {
                guiInputHandler.OnMainMenu = true;
            }
        }
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
        }
        if(flashlight != null)
        {
            flashlight.enabled = false;
        }
    }

    public void UnfreezePlayer()
    {
        DisableMovement = false;
        GameObject gui = GameObject.FindGameObjectWithTag("GUI");
        GetComponentInChildren<Raycaster>().EnableRaycaster();
        MouseLook mouseLook = gameObject.GetComponent<MouseLook>();
        Flashlight flashlight = GetComponentInChildren<Flashlight>();
        if (gui != null)
        {
            GUIInputHandler guiInputHandler = gui.GetComponent<GUIInputHandler>();
            if (guiInputHandler != null)
            {
                guiInputHandler.OnMainMenu = false;
            }
        }
        if (mouseLook != null)
        {
            mouseLook.enabled = true;
        }
        if (flashlight != null)
        {
            flashlight.enabled = true;
        }
    }

    public bool isNormalSpeed()
    {
        return currSpeed == normalSpeed;
    }
}