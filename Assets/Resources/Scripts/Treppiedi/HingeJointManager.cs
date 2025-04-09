using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HingeJointManager : MonoBehaviour
{
    public string tagToCheck = "X"; // Imposta il tag da controllare
    private XRGrabInteractable grabInteractable;
    

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Associa direttamente i metodi agli eventi
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    // Funzione chiamata quando l'oggetto viene afferrato
    private void OnGrab(SelectEnterEventArgs arg0)
    {
        if (gameObject.CompareTag(tagToCheck))
        {
            Debug.Log("Oggetto afferrato: " + gameObject.name);
            GrabObject();
        }
    }

    // Funzione chiamata quando l'oggetto viene rilasciato
    private void OnRelease(SelectExitEventArgs arg0)
    {
        if (gameObject.CompareTag(tagToCheck))
        {
            Debug.Log("Oggetto rilasciato: " + gameObject.name);
            ReleaseObject();
        }
    }

    // Funzione per gestire l'afferramento
    private void GrabObject()
    {
        Debug.Log("Oggetto afferrato con tag: " + tagToCheck);
    }

    // Funzione per gestire il rilascio
    private void ReleaseObject()
    {
        Debug.Log("Oggetto rilasciato con tag: " + tagToCheck);
    }
}