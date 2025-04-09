using UnityEngine;

public class UpdateHingeConnectedAnchor : MonoBehaviour
{
    public Transform corpoPrincipale; // Riferimento al corpo principale del cavalletto
    private Vector3 offsetIniziale;
    private HingeJoint hinge;
    private Vector3 anchorIniziale;


    void Start()
    {
        // Calcola l'offset iniziale tra il corpo principale e questa gamba
        offsetIniziale = transform.position - corpoPrincipale.position;
        hinge = GetComponent<HingeJoint>();
        anchorIniziale = hinge.anchor;
        hinge.autoConfigureConnectedAnchor = false;

    }

    void Update()
    {
        // Calcola la nuova posizione del connectedAnchor basata sulla posizione attuale del corpo principale
        Vector3 nuovaPosizioneAnchor = corpoPrincipale.position + offsetIniziale;
        transform.position = nuovaPosizioneAnchor;

        hinge.connectedAnchor = transform.position;
        
        // Ripristina la posizione dell'anchor
        //hinge.anchor = anchorIniziale;
        
        
    }
}