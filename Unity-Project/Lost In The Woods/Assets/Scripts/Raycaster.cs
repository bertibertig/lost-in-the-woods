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

    private Camera camera;
    [SerializeField, ReadOnly]
    private int charge;
    [SerializeField]
    private bool lookingAtGhost;
    private bool loadChargeStarted;

    private LevelExit levelExit = null;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        charge = 0;
        lookingAtGhost = false;
        loadChargeStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midPoint = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2);
        Ray ray = camera.ScreenPointToRay(midPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitTarget = hit.transform.gameObject;

            if (hitTarget.CompareTag("GhostEnemy"))
            {
                lookingAtGhost = true;
                if (!loadChargeStarted)
                {
                    StartCoroutine(LoadCharge());
                }
            }
            if(hitTarget.CompareTag("LevelExit"))
            {
                levelExit = hitTarget.GetComponent<LevelExit>();
                if(levelExit != null)
                {
                    levelExit.LookingAtObject();
                }
            }
            else
            {
                lookingAtGhost = false;
                loadChargeStarted = false;
                StopCoroutine(LoadCharge());
                charge = 0;
                if(chargeUIText != null)
                    chargeUIText.text = charge.ToString();
                
                if(levelExit != null)
                {
                    levelExit.LookingAway();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {

                if (hitTarget.CompareTag("GhostEnemy"))
                {
                    GhostHealthController healthController = hitTarget.GetComponent<GhostHealthController>();
                    healthController.DamageGhost(charge);
                    charge = 0;
                }
                if (hitTarget.CompareTag("LevelExit") && levelExit != null)
                {
                    levelExit.LookingAway();
                }
            }
        }
    }

    private IEnumerator LoadCharge()
    {
        loadChargeStarted = true;
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
}
