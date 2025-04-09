using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HingeUnlockButton : MonoBehaviour
{
    private XRBaseInteractable interactable;
    public GameObject targetLeg;
    private HingeJoint targetHingeJoint;

    void Start()
    {
        targetHingeJoint = targetLeg.GetComponent<HingeJoint>();
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);
        
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Pulsante premuto!");
        // Inserisci qui le azioni da eseguire quando il pulsante viene premuto
        
        if (targetHingeJoint != null)
        {
            // Ottieni i limiti attuali
            JointLimits limits = targetHingeJoint.limits;

            if (limits.min > 0f)
            {
                targetLeg.GetComponent<HingeOpeningLocker>().UnlockLeg();
                if(limits.min > 29f)
                    interactable.GetComponentInParent<FreezeRotationBody>().OnUnlockLeg();
            }
            
            // Imposta i nuovi valori per i limiti
            limits.min = 0f;  // Angolo minimo desiderato
            limits.max = 30f; // Angolo massimo desiderato

            // Applica i nuovi limiti al HingeJoint
            targetHingeJoint.limits = limits;

            // Abilita l'uso dei limiti
            targetHingeJoint.useLimits = true;
            
            
            
        }
        else
        {
            Debug.LogWarning("HingeJoint di destinazione non assegnato.");
        }
    }
}