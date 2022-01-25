using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string displayText;

    [SerializeField]
    private TextMeshProUGUI interactionText;

    [SerializeField]
    private string buttonName = "Interact";

    [SerializeField]
    private UnityEvent<GameObject> actionsToTakeOnButtonPress;

    public bool PlayerLookingAtObject { get; set; }

    void Start()
    {
        PlayerLookingAtObject = false;
        interactionText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(buttonName) && PlayerLookingAtObject)
        {
            actionsToTakeOnButtonPress.Invoke(gameObject);
        }
    }

    public void LookingAtObject()
    {
        PlayerLookingAtObject = true;
        interactionText.text = displayText;
        interactionText.enabled = true;
    }

    public void LookingAway()
    {
        PlayerLookingAtObject = false;
        interactionText.text = "";
        interactionText.enabled = false;
    }

    public void EnableInteraction()
    {
        gameObject.tag = "Interactable";

    }

    public void DisableInteraction()
    {
        gameObject.tag = "Empty";
    }
}
