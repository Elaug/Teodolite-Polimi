using UnityEngine;

public class BubbleLevelController : MonoBehaviour
{
    public Material bubbleMaterial; // Materiale con lo shader della bolla
    public float maxTilt = 60f; // Angolo massimo della livella in gradi
    public float bubbleScale = 1.5f;
    
    
    private float initialTiltX;
    private float initialTiltZ;

    // Converte un angolo nel range [-180, 180]
    private float GetSignedAngle(float angle)
    {
        return (angle > 180f) ? angle - 360f : angle;
    }

    void Start()
    {
        // Salva la rotazione globale (tara) in range [-180, 180]
        initialTiltX = GetSignedAngle(transform.eulerAngles.x);
        initialTiltZ = GetSignedAngle(transform.eulerAngles.z);
    }

    void Update()
    {
        // Calcola la rotazione globale corrente in range [-180, 180]
        float currentTiltX = GetSignedAngle(transform.eulerAngles.x);
        float currentTiltZ = GetSignedAngle(transform.eulerAngles.z);

        // Calcola la variazione rispetto al valore iniziale (tara)
        float tiltX = currentTiltX - initialTiltX;
        float tiltZ = currentTiltZ - initialTiltZ;

        // Limita gli angoli al range [-maxTilt, maxTilt]
        tiltX = Mathf.Clamp(tiltX, -maxTilt, maxTilt);
        tiltZ = Mathf.Clamp(tiltZ, -maxTilt, maxTilt);

        // Converti in radianti e calcola tan(θ)
        float dx = Mathf.Tan(tiltX * Mathf.Deg2Rad);
        float dz = Mathf.Tan(tiltZ * Mathf.Deg2Rad);

        // Applica il fattore di scala e normalizza la posizione nel range [0, 1]
        float x = 0.5f - (bubbleScale * dx) / 2;
        float y = 0.5f - (bubbleScale * dz) / 2;

        // Imposta la posizione della bolla
        Vector4 bubbleCenter = new Vector4(y, x, 0f, 0f);
        bubbleMaterial.SetVector("_BubbleCenter", bubbleCenter);
    }
}