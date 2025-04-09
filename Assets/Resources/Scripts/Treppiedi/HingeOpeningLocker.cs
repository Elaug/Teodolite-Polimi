using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HingeOpeningLocker : MonoBehaviour
{
    
    private HingeJoint hinge;
    private XRGrabInteractable grabInteractable;
    private bool locked;
    private bool isGrabbed;
    private bool alreadyLocked = false;

    // Angolo massimo consentito (ad es. 30°)
    public float maxAngle = 30f; 

    void Start() {
        hinge = GetComponent<HingeJoint>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        locked = false;

        // Disabilita motor e spring per non forzare il ritorno all'angolo 0
        hinge.useSpring = false;
        hinge.useMotor = false;

        // Imposta i limiti iniziali (da 0 a maxAngle)
        JointLimits limits = hinge.limits;
        hinge.useLimits = true;
        limits.min = 0f;
        limits.max = maxAngle;
        hinge.limits = limits;

        // Al rilascio, aggiorna il limite inferiore all'angolo attuale
        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs arg0)
    {
        isGrabbed = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Prendi l'angolo corrente della gamba
        float currentAngle = hinge.angle;

        // Aggiorna il limite inferiore solo se l'angolo corrente è maggiore dell'attuale limite minimo
        JointLimits limits = hinge.limits;
        if (currentAngle >= limits.min && currentAngle < maxAngle)
        {
            limits.min = currentAngle;
            hinge.limits = limits;
        }

        if (currentAngle >= (maxAngle-1f) && hinge.limits.min > 0)
        {
            
            locked = true;
            Debug.Log("Try to lock leg");
            if (!alreadyLocked)
            {
                gameObject.GetComponentInParent<FreezeRotationBody>().OnLockLeg();
                alreadyLocked = true;
            }
            
            hinge.GetComponent<Rigidbody>().useGravity = false;
            hinge.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;  
            hinge.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
            
        }

        isGrabbed = false;

    }
    
    public void UnlockLeg()
    {
        alreadyLocked = false;
    }
    
    
    

    private void Update()
    {
        /*
        // Prendi l'angolo corrente della gamba
        float currentAngle = hinge.angle;
        if (isGrabbed)
        {
            if (currentAngle >= maxAngle)
            {
                //hinge.angle = maxAngle;
                grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
                
            }
        }
        */

    } 
}

