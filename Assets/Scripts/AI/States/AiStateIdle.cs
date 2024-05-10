using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateIdle : MonoBehaviour, IAiState
{
    public string activeState;
    private AiBehavior aiBehavior;
   

    void Awake () {
        aiBehavior = GetComponent<AiBehavior> ();
        
        Debug.Assert (aiBehavior, "Wrong initial parameters");
    }

    public void OnStateEnter (string previousState, string newState) {

    }
    public void OnStateExit (string previousState, string newState) {

    }
    public void TriggerEnter(Collider2D my, Collider2D other) {

    }
    public void TriggerStay(Collider2D my, Collider2D other) {
        aiBehavior.ChangeState(activeState);
    }

    public void TriggerExit(Collider2D my, Collider2D other) {

    }
}
