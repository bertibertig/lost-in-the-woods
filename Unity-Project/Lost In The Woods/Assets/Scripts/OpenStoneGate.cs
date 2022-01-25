using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStoneGate : MonoBehaviour
{
    public int downSpeed = 2;
    [SerializeField]
    private string[] dialogToDisplayIfKeysNotFound;

    private DialogeHandler dialogeHandler;

    private void Start()
    {
        dialogeHandler = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogeHandler>();
    }

    public void openGate()
    {
        if (CollectKeys.keysFound >= 3)
        {
            StartCoroutine(moveGateDown());
        }
        else
        {
            dialogeHandler.StartDialog(dialogToDisplayIfKeysNotFound);
        }
    }

    private IEnumerator moveGateDown()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            this.transform.position += Vector3.down * Time.deltaTime * downSpeed;
        }
    }
}
