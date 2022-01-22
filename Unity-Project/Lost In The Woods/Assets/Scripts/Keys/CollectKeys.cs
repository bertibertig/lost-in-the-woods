using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKeys : MonoBehaviour
{
    public static int keysFound;
    public AudioSource pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        keysFound = 0;
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.tag == "Item")
        {
            keysFound++;
            pickUpSound.Play();
            Destroy(item.gameObject);
        }
    }
}
