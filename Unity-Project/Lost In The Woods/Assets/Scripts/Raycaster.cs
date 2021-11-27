using System.Collections;
using UnityEngine;
using Unity.Collections;
using TMPro;

public class Raycaster : MonoBehaviour
{
    [Range(0.1f,1)]
    public float speed = 1;
    public TextMeshProUGUI chargeUIText;

    private Camera camera;
    [SerializeField,ReadOnly]
    private int charge;
    [SerializeField]
    private bool lookingAtGhost;
    private bool loadChargeStarted;

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
                if(!loadChargeStarted)
                {
                    StartCoroutine(LoadCharge());
                }
            }
            else
            {
                lookingAtGhost = false;
                loadChargeStarted = false;
                StopCoroutine(LoadCharge());
                charge = 0;
                chargeUIText.text = charge.ToString();
            }

                if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Hit: " + hit.point);
                StartCoroutine(HitIndicator(hit.point));

                if (hitTarget.tag == "GhostEnemy")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    private IEnumerator HitIndicator(Vector3 hitLocation)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = hitLocation;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(MoveShphere(sphere, 5));

    }

    private IEnumerator MoveShphere(GameObject sphere, int secs)
    {
        int counter = 0;

        while (counter <= secs)
        {
            sphere.transform.position = sphere.transform.position + new Vector3(1, 0, 0);

            counter++;
            yield return new WaitForSeconds(1.0f);
        }

        Destroy(sphere);
    }

    private IEnumerator LoadCharge()
    {
        loadChargeStarted = true;
        while (lookingAtGhost && charge <= 100)
        {
            yield return new WaitForSeconds(speed);
            charge++;
            if(chargeUIText != null)
            {
                chargeUIText.text = charge.ToString();
            }
        }
    }
}
