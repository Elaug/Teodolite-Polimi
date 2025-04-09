using System;
using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ConfLockButtonZ: MonoBehaviour
{
    public GameObject associatedLeg;
    private XRBaseInteractable interactable;
    private ConfigurableJoint targetConfigurableJoint;  // Riferimento al ConfigurableJoint dell'altro oggetto
    private Vector3 startPosition;
    
    private bool locked;
    private bool isRotating;
    
    public float rotationSpeed = 250f;
    private float movementSpeed = 0.01f;
    
    private float initialYPosition;
    public float translationLimit = 1f;
    
    public float openDuration = 2f;
    public float closeDuration = 2f;
    
    //if = 1 it goes forward, if = -1 it goes backward
    private int sign = 1;
    
    void Start()
    {
        targetConfigurableJoint = associatedLeg.GetComponent<ConfigurableJoint>();
        
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);
        
        locked = true;
        isRotating = false;

        startPosition = targetConfigurableJoint.connectedAnchor;
        initialYPosition = transform.localPosition.y;
    }
    

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        StartCoroutine(locked ? Open() : Close());
        Debug.Log("Pulsante premuto!");

        //SoftJointLimit limit = targetConfigurableJoint.linearLimit;
        //float actualZ = targetConfigurableJoint.targetPosition.z;
    }

    IEnumerator Open()
    {
        BoxCollider screw = transform.GetComponent<BoxCollider>();
        screw.enabled = false;

        float currentYPosition = transform.localPosition.y;
        float translationAlongY = currentYPosition - initialYPosition;
        float positiveTay = Math.Abs(translationAlongY);
        
        float elapsedTime = 0f;

        // Mentre l'oggetto non ha raggiunto il limite di traslazione
        while (elapsedTime < openDuration)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            transform.Translate(0, -movementSpeed * Time.deltaTime, 0);

            // Aggiorna la posizione attuale e ricalcola il valore di positiveTay
            currentYPosition = transform.localPosition.y;
            translationAlongY = currentYPosition - initialYPosition;
            positiveTay = Math.Abs(translationAlongY);
            
            elapsedTime += Time.deltaTime;
            
            Debug.Log($"Translation Along Y: {translationAlongY}, Positive Tay: {positiveTay}");

            yield return null; // Aspetta il frame successivo
        }

        screw.enabled = true;
        
        //********************************************************
        OpenLeg();
        //********************************************************
        
        locked = false;
        
    }


    IEnumerator Close()
    {
        BoxCollider screw = transform.GetComponent<BoxCollider>();
        screw.enabled = false;

        float currentYPosition = transform.localPosition.y;
        float translationAlongY = currentYPosition - initialYPosition;
        float positiveTay = Math.Abs(translationAlongY);
        
        float elapsedTime = 0f;

        // Mentre l'oggetto non ha raggiunto la posizione desiderata
        while (elapsedTime < closeDuration)
        {
            transform.Rotate(0, - rotationSpeed * Time.deltaTime, 0);
            transform.Translate(0, movementSpeed * Time.deltaTime, 0);

            // Aggiorna la posizione attuale e ricalcola il valore di positiveTay
            currentYPosition = transform.localPosition.y;
            translationAlongY = currentYPosition - initialYPosition;
            positiveTay = Math.Abs(translationAlongY);
            
            elapsedTime += Time.deltaTime;
            
            Debug.Log($"Positive Tay: {positiveTay}");

            yield return null; // Aspetta il frame successivo
        }
        
        screw.enabled = true;
        
        //********************************************************
        LockLeg();
        //********************************************************
        
        locked = true;
    }

    private void LockLeg(){
        
        Vector3 currentGlobalPos = associatedLeg.transform.position;
        Vector3 currentAnchor = associatedLeg.transform.parent.InverseTransformPoint(currentGlobalPos);
        targetConfigurableJoint.connectedAnchor = currentAnchor;
        targetConfigurableJoint.yMotion = ConfigurableJointMotion.Locked;
        
        
        //associatedLeg.GetComponent<Rigidbody>().useGravity = false;
        //associatedLeg.GetComponent<ExeManager>().setLocked(true);
        //associatedLeg.GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void OpenLeg()
    {
        
        targetConfigurableJoint.connectedAnchor = startPosition;
        targetConfigurableJoint.yMotion = ConfigurableJointMotion.Limited;
        
        //associatedLeg.GetComponent<Rigidbody>().useGravity = true;
        //associatedLeg.GetComponent<ExeManager>().setLocked(false);
        //associatedLeg.GetComponent<XRGrabInteractable>().enabled = true;
    }
    
}
