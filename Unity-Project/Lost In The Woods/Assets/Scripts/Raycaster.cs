using System.Collections;
using UnityEngine;
using Unity.Collections;
using TMPro;

public class Raycaster : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float speed = 1;
    public int cooldown = 3;
    [SerializeField]
    private float minDistance = 3.0f;

    private Camera camera;
    [SerializeField, ReadOnly]
    private int charge;
    private bool loadChargeStarted;
    private bool raycasterDisabled;

    private LevelExit levelExit = null;
    private Interactable interactable = null;
    private GUIInputHandler guiInputHandler;
    private Task loadCharge;

    //camera UI static values
    private static int MAX_CHARGE_PER_SHOT = 8;
    private static float INITIAL_ALPHA_VALUE_OF_SPIRIT_BALLS = 0.5f;
    private static float FINAL_ALPHA_VALUE_OF_SPIRIT_BALLS = 1;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        guiInputHandler = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIInputHandler>();
        charge = 0;
        loadChargeStarted = false;
        raycasterDisabled = false;
        loadCharge = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!raycasterDisabled)
        {
            Vector3 midPoint = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2);
            Ray ray = camera.ScreenPointToRay(midPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitTarget = hit.transform.gameObject;
                float distance = Vector3.Distance(gameObject.transform.position, hitTarget.transform.position);

                if (hitTarget.CompareTag("GhostEnemy") && guiInputHandler.CameraDisplayed)
                {
                    if (!loadChargeStarted)
                    {
                        loadChargeStarted = true;
                        loadCharge = new Task(LoadCharge());
                    }
                }
                else if (hitTarget.CompareTag("LevelExit") && distance <= minDistance)
                {
                    levelExit = hitTarget.GetComponent<LevelExit>();
                    if (levelExit != null)
                    {
                        levelExit.LookingAtObject();
                    }
                }
                else if (hitTarget.CompareTag("Interactable") && distance <= minDistance)
                {
                    interactable = hitTarget.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.LookingAtObject();
                    }
                }
                else
                {
                    loadChargeStarted = false;
                    if (loadCharge != null && loadCharge.Running)
                    {
                        loadCharge.Stop();
                    }
                    charge = 0;

                    if (levelExit != null && levelExit.PlayerLookingAtObject)
                    {
                        levelExit.LookingAway();
                    }
                    if (interactable != null && interactable.PlayerLookingAtObject)
                    {
                        interactable.LookingAway();
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (hitTarget.CompareTag("GhostEnemy") && guiInputHandler.CameraDisplayed)
                    {
                        GhostHealthController ghostHealthController = hitTarget.GetComponent<GhostHealthController>();
                        print("Ghost HP: " + ghostHealthController.Hp);
                        ghostHealthController.DamageGhost(charge);
                        loadCharge.Stop();
                        charge = 0;
                        ResetGhostBalls();
                        loadCharge = new Task(LoadCharge());
                    }
                    if (hitTarget.CompareTag("LevelExit") && levelExit != null)
                    {
                        levelExit.LookingAway();
                    }
                }
            }
        }
    }

    private void ResetGhostBalls()
    {
        foreach(var ball in guiInputHandler.GhostBalls)
        {
            ball.color = new Color(Color.white.r, Color.white.g, Color.white.b, INITIAL_ALPHA_VALUE_OF_SPIRIT_BALLS);
        }
    }

    private IEnumerator LoadCharge()
    {
        while (charge < MAX_CHARGE_PER_SHOT)
        {
            yield return new WaitForSeconds(speed);
            guiInputHandler.GhostBalls[charge].color = new Color(Color.white.r, Color.white.g, Color.white.b, FINAL_ALPHA_VALUE_OF_SPIRIT_BALLS);
            charge++;
        }
    }

    public void DisableRaycaster()
    {
        raycasterDisabled = true;
    }

    public void EnableRaycaster()
    {
        raycasterDisabled = false;
    }
}
