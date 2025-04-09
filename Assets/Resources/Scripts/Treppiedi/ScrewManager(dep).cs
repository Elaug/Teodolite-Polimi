using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScrewManager : MonoBehaviour
{
    
    private XRGrabInteractable grabInteractable;
    private bool grabbed;

    private Vector3 startingPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnButtonPressed);
        grabInteractable.selectExited.AddListener(OnRelease);

        grabbed = false;
        
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
       
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        startingPosition = transform.position;
        
        grabbed = true;
        
    }
    
    private void OnRelease(SelectExitEventArgs args)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        grabbed = false;
    }

    private void LateUpdate()
    {
        if (grabbed)
        {
            transform.position = startingPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
