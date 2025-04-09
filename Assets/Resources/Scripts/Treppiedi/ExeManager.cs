using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ExeManager : MonoBehaviour
{
    private bool isLocked;
     public GameObject lockerBall; // Riferimento all'oggetto mesh da mostrare/nascondere

    // Start is called before the first frame update
    void Start()
    {
        // Assicurati che l'oggetto mesh sia nascosto all'inizio
        if (lockerBall != null)
        {
            lockerBall.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Verifico che il collider sia quello del pavimento
        if (other.collider.CompareTag("Ground"))
        {
            // Se tocco il pavimento, mostro l'oggetto mesh
            if (lockerBall != null)
            {
                lockerBall.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // Verifico che il collider sia quello del pavimento
        if (other.collider.CompareTag("Ground"))
        {
            // Se non tocco più il pavimento, nascondo l'oggetto mesh
            if (lockerBall != null)
            {
                lockerBall.SetActive(false);
            }
        }
    }
}