using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FreezeRotationBody : MonoBehaviour
{
    
    private XRGrabInteractable grabInteractable;
    private Vector3 startingRotation;
    private Vector3 startingPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        startingRotation = transform.rotation.eulerAngles;

        lockedLegs = false;
        /*
        if (grabInteractable != null)
        {
            // Associa direttamente i metodi agli eventi
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        */
    }
    /*
    // Funzione chiamata quando l'oggetto viene afferrato
    private void OnGrab(SelectEnterEventArgs arg0)
    {
        if (!lockedLegs)
        {
            transform.rotation = Quaternion.Euler(startingRotation);
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
        
    }

    // Funzione chiamata quando l'oggetto viene rilasciato
    private void OnRelease(SelectExitEventArgs arg0)
    {
        if (lockedLegs)
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
        
    }
    */

    private int lockCounter;
    private bool lockedLegs;

    public void OnLockLeg()
    {
        lockCounter++;
        if (lockCounter == 3)
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
        Debug.Log(lockCounter);
        
    }
    

    public void OnUnlockLeg()
    {
        lockCounter--;
        if (lockCounter < 3)
        {
            transform.rotation = Quaternion.Euler(startingRotation);
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
        Debug.Log(lockCounter);

    }

    /*
    private void Update()
    {
        if (lockCounter == 3)
        {
            lockedLegs = true;
        }
        else if (lockCounter is < 3 and >= 0)
        {
            lockedLegs = false;
        }
        else
        {
            Debug.Log("Numero di gambe bloccate non coerente.\n Gambe bloccate: " + lockCounter);
            //throw new Exception("Numero di gambe bloccate non coerente.\n Gambe bloccate: " + lockCounter);
        }
    }
    */
}
