using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LegPositionLocker : MonoBehaviour
{
    private XRBaseInteractable interactable;
    public GameObject leg; // Riferimento all'oggetto mesh da mostrare/nascondere
    
    private bool locked = false;
    
    
    void Start()
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

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Interaction started!");
        // Mostra l'oggetto mesh solo se sta toccando il pavimento
        if (leg != null)
        {
            if (locked)
            {
                leg.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                leg.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            
            locked = !locked;
            
        }
    }
    
}