using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKeys : MonoBehaviour
{
    public static int keysFound;

    // Start is called before the first frame update
    void Start()
    {
        keysFound = 0;
    }

    private void Update()
    {
        if (keysFound == 1)
        {
            Debug.Log("Everything Found");
        }
        else Debug.Log("Failed");
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.tag == "Item")
        {
            keysFound++;
            Destroy(item.gameObject);
        }
    }
}
