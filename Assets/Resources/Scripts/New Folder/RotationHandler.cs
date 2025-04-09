using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    
    
    /*
     *Le tre viti si considerano come se fossero a
     * Vite 1 : -0.85 , 0
     * Vite 2 : 0.425 , 0.736
     * Vite 3 : 0.425, -0.736
     *
     * se ho una variazione rispetto a un vertice X ho che questo vertice formerà un angolo di inclinazione
     * che ha come direzione il raggio dal vertice al centro. Posso quindi calcolare questo angolo come
     * arcoseno della differenza di altezza su y diviso il raggio.
     * Trovato questo angolo chiamato theta, posso calcolarne le componenti su x e z.
     * Per farlo devo prima considerare l'angolo alpha che sarebbe quello sotteso tra il raggio del vertice X e l'asse x.
     * In questo caso avendo un triangolo equilatero questo angolo sarà 60° o 0°.
     * Una volta fatto questo avro che le due variazioni di inclinazione saranno:
     * theta(z) = theta x cos(alpha)
     * theta(x) = -theta x sin(alpha)
     *
     * in questo script creo 3 metodi richiamabili dalle viti in caso di rotazione dove far ritornare
     * la propria posizione e l'incremento di y.
     * i metodi produrranno in uscita una variazione di inclinazione dell'oggetto su x e z.
     *
     * Devo clampare i movimenti in positivo e negativo partendo dalla condizione iniziale che è 0,0,0
     * per farlo posso definire per ogni vertice un range di movimento e aggiornarlo ogni volta che succede.
     * Una volta raggiunto il limite dei movimenti posso decidere se bloccare il movimento in quella direzione
     * oppure bloccare la possibilità di ruotare le viti.
     *
     * Da verificare cosa succede nel caso in cui vengano girate due viti contemporaneamente.
     */
    
    Vector2 vite1 = new Vector2(-0.847f, 0f);
    Vector2 vite2 = new Vector2(0.434f, 0.688f);
    Vector2 vite3 = new Vector2(0.396f, -0.748f);
    
    private float r = -0.85f; // raggio del triangolo equilatero
    public float maxAngle = 20f; // Angolo massimo di rotazione
    
    // Variabili che tengono il contributo di ogni vite
    private Vector3 rotationViteOne = Vector3.zero;
    private Vector3 rotationViteTwo = Vector3.zero;
    private Vector3 rotationViteThree = Vector3.zero;
    
    
    private float CalculateAlpha(float x, float z)
    {
        return Mathf.Atan2(z, x);
    }
    
    private float MapValueToRotation(float value)
    {
        // Mappa il valore da -0.5 a 0.5 all'intervallo di rotazione da -maxAngle a maxAngle
        return Mathf.Lerp(-maxAngle, maxAngle, Mathf.InverseLerp(-0.5f, 0.5f, value));
    }
    
    private void UpdateCumulativeRotation()
    {
        // Somma il contributo di tutte le viti per ottenere una rotazione cumulativa
        Vector3 combinedRotation = rotationViteOne + rotationViteTwo + rotationViteThree;
        transform.rotation = Quaternion.Euler(combinedRotation);
    }
    
    
    
    public void SetViteOne(float deltaRotationY)
    {
        float deltaY = deltaRotationY;
        float theta = Mathf.Asin(deltaY / r);
        float alpha = CalculateAlpha(vite1.x, vite1.y);
        float thetaX = -theta * Mathf.Sin(alpha);
        float thetaZ = theta * Mathf.Cos(alpha);

        Vector3 rotation = new Vector3(thetaX, 0, thetaZ);

        float mappedRotation = Mathf.Abs(MapValueToRotation(deltaRotationY));
        
        rotationViteOne = new Vector3(thetaX, 0, thetaZ) * mappedRotation;
        UpdateCumulativeRotation();
    }

    public void SetViteTwo(float deltaRotationY)
    {
        float deltaY = deltaRotationY;
        float theta = Mathf.Asin(deltaY / r);
        float alpha = CalculateAlpha(vite2.x, vite2.y);
        float thetaX = -theta * Mathf.Sin(alpha);
        float thetaZ = theta * Mathf.Cos(alpha);

        Vector3 rotation = new Vector3(thetaX, 0, thetaZ);

        float mappedRotation = Mathf.Abs(MapValueToRotation(deltaRotationY));

        rotationViteTwo = new Vector3(thetaX, 0, thetaZ) * mappedRotation;
        UpdateCumulativeRotation();
    }

    public void SetViteThree(float deltaRotationY)
    {
        float deltaY = deltaRotationY;
        float theta = Mathf.Asin(deltaY / r);
        float alpha = CalculateAlpha(vite3.x, vite3.y);
        float thetaX = -theta * Mathf.Sin(alpha);
        float thetaZ = theta * Mathf.Cos(alpha);

        Vector3 rotation = new Vector3(thetaX, 0, thetaZ);

        float mappedRotation = Mathf.Abs(MapValueToRotation(deltaRotationY));

        rotationViteThree = new Vector3(thetaX, 0, thetaZ) * mappedRotation;
        UpdateCumulativeRotation();
    }

    public void ScrewRotate(float deltaRotationY, int index)
    {
        if (index == 0)
        {
            SetViteOne(deltaRotationY);
        }
        else if (index == 1)
        {
            SetViteTwo(deltaRotationY);
        }
        else if (index == 2)
        {
            SetViteThree(deltaRotationY);
        }
        else
        {
            Debug.Log("Errore: indice vite non valido.");
        }
        
        
        
        
    }
    
}
