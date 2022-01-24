using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibilty : MonoBehaviour
{
    public List<GameObject> changeVisibility;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in changeVisibility)
        {
            obj.SetActive(false);
        }
    }

    public void enableVisibility()
    {
        foreach (GameObject obj in changeVisibility)
        {
            obj.SetActive(true);
        }
    }
}
