using Unity.VRTemplate;
using UnityEngine;

public class ScrewRoller : MonoBehaviour
{
    
    public RotationHandler rotationHandler;
    
    /*
     * In questa classe devo gestire il movimento della vite.
     * Devo capire la mappatura tra la rotazione della vite e l'aumento del delta y nel rotationHandler.
     * Quindi avro una rotazione possibile solo attorno all'asse Y. Questa rotazione deve generare un valore
     * deltaY. 
     * 
     */

    private XRKnob knob;

    
    private float scaleFactor = 100f; // Fattore di scala per la conversione
    public int index = 0;
    
    private float previousIncrement = 0.5f; // Incremento precedente dell'oggetto
    
    
    private float previousRotationY; // Rotazione precedente dell'oggetto
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        knob = GetComponent<XRKnob>();
        knob.onValueChange.AddListener(OnKnobValueChanged);
    }

    private void OnKnobValueChanged(float arg0)
    {
        
        //Debug.Log($"OnKnobValueChanged: {arg0}");
        rotationHandler.ScrewRotate((arg0 - 0.5f), index);
        
    }

 

    
    
}  
