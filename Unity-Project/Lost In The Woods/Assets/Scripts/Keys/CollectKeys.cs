using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKeys : MonoBehaviour
{
    public static int keysFound;
    public AudioClip pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        keysFound = 0;
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = pickUpSound;
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.tag == "Item")
        {
            keysFound++;
            GetComponent<AudioSource>().Play();
            Destroy(item.gameObject);
        }
    }
}
