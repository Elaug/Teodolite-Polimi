using UnityEngine;
    using System.Collections;
    
    public class VRInteractionSequence : MonoBehaviour
    {
        private enum State
        {
            SelezionaPunto,
            PrendiTreppiedi,
            CentraTreppiedi,
            AgganciaTeodolite,
            StazionaBasamento,
            Completato
        }
    
        private State currentState = State.SelezionaPunto;
        private Vector3 targetPoint;
        // Riferimento all'oggetto da attivare
        public GameObject tripodSystem;
        public GameObject teodolite;
        
        
        // Flag per il completamento dell'interazione
        private bool puntoSelezionato = false, treppiediPreso = false, treppiediCentrato = false;
    
        void Start()
        {
            StartCoroutine(ProcessSequence());
        }
    
        private IEnumerator ProcessSequence()
        {
            while (currentState != State.Completato)
            {
                switch (currentState)
                {
                    case State.SelezionaPunto:
                        yield return StartCoroutine(SelezionaPunto());
                        break;
                    case State.PrendiTreppiedi:
                        yield return StartCoroutine(PrendiTreppiedi());
                        break;
                    case State.CentraTreppiedi:
                        yield return StartCoroutine(CentraTreppiedi());
                        break;
                    case State.AgganciaTeodolite:
                        yield return StartCoroutine(AgganciaTeodolite());
                        break;
                    case State.StazionaBasamento:
                        yield return StartCoroutine(StazionaBasamento());
                        break;
                }
                yield return null;
            }
            Debug.Log("Sequenza completata");
        }
    
        // Metodo che simula la selezione del punto sul terreno tramite interazione dell'utente
        private IEnumerator SelezionaPunto()
        {
            Debug.Log("Attesa per la selezione del punto sul terreno...");
            puntoSelezionato = false;

            // Attende fino a quando il flag non diventa true
            yield return new WaitUntil(() => puntoSelezionato);
    
            targetPoint = new Vector3(0, 0, 0); // Imposta il punto scelto
            currentState = State.PrendiTreppiedi;
        }
        
    
        private IEnumerator PrendiTreppiedi()
        {
            Debug.Log("Attesa per l'interazione con il treppiedi...");
            treppiediPreso = false;
            
            // Controlla che il riferimento sia valido
            if (tripodSystem != null)
            {
                // Attiva l'oggetto nella scena
                tripodSystem.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Il riferimento a targetObject non è stato assegnato.");
            }
            
            
            // Attende fino a quando il flag non diventa true
            yield return new WaitUntil(() => puntoSelezionato);
            
            currentState = State.CentraTreppiedi;
        }
    
        private IEnumerator CentraTreppiedi()
        {
            Debug.Log("Centrare il treppiedi sul punto selezionato...");
            yield return new WaitForSeconds(2f);  // Simula l'interazione
            currentState = State.AgganciaTeodolite;
        }
        
        private IEnumerator AgganciaTeodolite()
        {
            // Controlla che il riferimento sia valido
            if (teodolite != null)
            {
                // Attiva l'oggetto nella scena
                teodolite.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Il riferimento a targetObject non è stato assegnato.");
            }
            
            
            
            yield return new WaitUntil(() => puntoSelezionato);
            currentState = State.StazionaBasamento;
        }

        private IEnumerator StazionaBasamento()
        {
            yield return new WaitUntil(() => puntoSelezionato);
            currentState = State.Completato;
        }
        
        
        
        // Setter per modificare il flag da un'altra classe
        public void SetPointSelected()
        {
            puntoSelezionato = true;
        }
        
        public void SetTripodLocked()
        {
            treppiediPreso = true;
        }
        
        
    }