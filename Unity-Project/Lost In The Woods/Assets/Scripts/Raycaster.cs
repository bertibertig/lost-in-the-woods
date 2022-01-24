using System.Collections;
using UnityEngine;
using Unity.Collections;
using TMPro;

public class Raycaster : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float speed = 1;
    public TextMeshProUGUI chargeUIText;
    public int cooldown = 3;
    [SerializeField]
    private float minDistance = 3.0f;

    private Camera camera;
    [SerializeField, ReadOnly]
    private int charge;
    private bool lookingAtGhost;
    private bool loadChargeStarted;
    private bool raycasterDisabled;

    private LevelExit levelExit = null;
    private Interactable interactable = null;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        charge = 0;
        lookingAtGhost = false;
        loadChargeStarted = false;
        raycasterDisabled = false;
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

                if (hitTarget.CompareTag("GhostEnemy"))
                {
                    lookingAtGhost = true;
                    if (!loadChargeStarted)
                    {
                        loadChargeStarted = true;
                        StartCoroutine(LoadCharge());
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
                    lookingAtGhost = false;
                    loadChargeStarted = false;
                    StopCoroutine(LoadCharge());
                    charge = 0;
                    if (chargeUIText != null)
                        chargeUIText.text = charge.ToString();

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
                    if (hitTarget.CompareTag("GhostEnemy"))
                    {
                        GhostHealthController healthController = hitTarget.GetComponent<GhostHealthController>();
                        print("Ghost HP: " + healthController.Hp);
                        healthController.DamageGhost(charge);
                        lookingAtGhost = false;
                        StopCoroutine(LoadCharge());
                        charge = 0;
                    }
                    if (hitTarget.CompareTag("LevelExit") && levelExit != null)
                    {
                        levelExit.LookingAway();
                    }
                }
            }
        }
    }

    private IEnumerator LoadCharge()
    {
        while (lookingAtGhost && charge <= 100)
        {
            yield return new WaitForSeconds(speed);
            charge++;
            if (chargeUIText != null)
            {
                chargeUIText.text = charge.ToString();
            }
        }
    }

    public void DisableRaycaster()
    {
        raycasterDisabled = true;
        if (chargeUIText != null)
        {
            chargeUIText.enabled = false;
        }
    }

    public void EnableRaycaster()
    {
        raycasterDisabled = false;
        if (chargeUIText != null)
        {
            chargeUIText.enabled = true;
        }
    }
}
