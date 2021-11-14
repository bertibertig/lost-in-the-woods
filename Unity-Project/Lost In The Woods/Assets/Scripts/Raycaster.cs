using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 midPoint = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2);
            Ray ray = camera.ScreenPointToRay(midPoint);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.point);
                StartCoroutine(HitIndicator(hit.point));
            }
        }
    }

    private IEnumerator HitIndicator(Vector3 hitLocation)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = hitLocation;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(MoveShphere(sphere,5));

    }

    private IEnumerator MoveShphere(GameObject sphere, int secs)
    {
        int counter = 0;

        while(counter <=  secs)
        {
            sphere.transform.position = sphere.transform.position + new Vector3(1, 0, 0);

            counter++;
            yield return new WaitForSeconds(1.0f);
        }

        Destroy(sphere);
    }
}
