using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ConfLockButton : MonoBehaviour
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
    public float translationLimit = 0.001f;

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
        initialYPosition = transform.localPosition.z;

    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        StartCoroutine(locked ? Open() : Close());
        Debug.Log("Pulsante premuto!");
  
    }
    
    IEnumerator Open()
    {
        //associatedLeg.GetComponent<Rigidbody>().isKinematic = false;
        BoxCollider screw = transform.GetChild(0).GetComponent<BoxCollider>();
        screw.enabled = false;

        float currentYPosition = transform.localPosition.z;
        float translationAlongY = currentYPosition - initialYPosition;
        float positiveTay = Math.Abs(translationAlongY);

        // Mentre l'oggetto non ha raggiunto il limite di traslazione
        while (positiveTay < translationLimit)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            transform.Translate(0, 0, -movementSpeed * Time.deltaTime);

            // Aggiorna la posizione attuale e ricalcola il valore di positiveTay
            currentYPosition = transform.localPosition.z;
            translationAlongY = currentYPosition - initialYPosition;
            positiveTay = Math.Abs(translationAlongY);

            yield return null; // Aspetta il frame successivo
        }

        //********************************************************
        OpenLeg();
        //********************************************************
        
        locked = false;
        screw.enabled = true;
    }


    IEnumerator Close()
    {
        BoxCollider screw = transform.GetChild(0).GetComponent<BoxCollider>();
        screw.enabled = false;

        float currentYPosition = transform.localPosition.z;
        float translationAlongY = currentYPosition - initialYPosition;
        float positiveTay = Math.Abs(translationAlongY);

        // Mentre l'oggetto non ha raggiunto la posizione desiderata
        while (positiveTay > 0.0001)
        {
            transform.Rotate(- rotationSpeed * Time.deltaTime, 0, 0);
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);

            // Aggiorna la posizione attuale e ricalcola il valore di positiveTay
            currentYPosition = transform.localPosition.z;
            translationAlongY = currentYPosition - initialYPosition;
            positiveTay = Math.Abs(translationAlongY);

            yield return null; // Aspetta il frame successivo
        }
        
        //********************************************************
        LockLeg();
        //********************************************************
        
        locked = true;
        screw.enabled = true;
    }
    
    private void LockLeg(){
        
        Vector3 currentGlobalPos = associatedLeg.transform.position;
        Vector3 currentAnchor = associatedLeg.transform.parent.InverseTransformPoint(currentGlobalPos);
        targetConfigurableJoint.connectedAnchor = currentAnchor;
        targetConfigurableJoint.yMotion = ConfigurableJointMotion.Locked;
    }

    private void OpenLeg()
    {
        
        targetConfigurableJoint.connectedAnchor = startPosition;
        targetConfigurableJoint.yMotion = ConfigurableJointMotion.Limited;
    }

}