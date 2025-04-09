using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DynamicGrabAttach : MonoBehaviour
{
    private XRBaseInteractable interactable;
    public GameObject grabPoint;
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject != null)
        {
            Vector3 localGrabPosition = transform.InverseTransformPoint(args.interactorObject.transform.position);
            grabPoint.transform.localPosition = localGrabPosition;
        }
    }

    private void Start()
    {
        // Ottieni il riferimento all'XRSimpleInteractable se non è già stato assegnato
        interactable = GetComponent<XRBaseInteractable>();
        
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            Debug.LogError("XRSimpleInteractable not found on this GameObject.");
        }
    }
}