using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehavior : MonoBehaviour
{
    public string defaultState;
    private IAiState previousState;
    private IAiState currentState;
    private List<IAiState> aiStates = new List<IAiState>();

    void Start()
    {
        IAiState[] states = GetComponents<IAiState>();
        if (states.Length > 0) {
            foreach (IAiState state in states) {
                aiStates.Add(state);
            }
            if (defaultState != null) {
                previousState = currentState = GetComponent(defaultState) as IAiState;
                if (currentState != null) {
                    ChangeState(currentState.GetType().ToString());
                } else {
                    Debug.LogError("Wrong default state");
                }
            } else {
                Debug.LogError("No default state");
            }
        }
    }

    public void ChangeState(string state) {
        if (state != "") {
            foreach (IAiState aiState in aiStates) {
                previousState = currentState;
                currentState = aiState;
                NotifyStateExit();
                DisableAllState();
                EnableNewState();
                NotifyStateEnter();
                return;
            }
        }
    }

    public void GotoDefaultState() {
        previousState = currentState;
        currentState = GetComponent(defaultState) as IAiState;
        NotifyStateExit();
        DisableAllState();
        EnableNewState();
        NotifyStateEnter();
    }

    public void DisableAllState() {
        foreach (IAiState aiState in aiStates) {
            MonoBehaviour state = GetComponent(aiState.GetType().ToString()) as MonoBehaviour;
            state.enabled = false;
        }
    }
    
    public void EnableNewState() {
        MonoBehaviour state = GetComponent(currentState.GetType().ToString()) as MonoBehaviour;
        state.enabled = true;
    }

    private void NotifyStateExit() {
        string prevS = previousState.GetType().ToString();
        string activeS = currentState.GetType().ToString();
        IAiState state = GetComponent(prevS) as IAiState;
        state.OnStateExit(prevS, activeS);
    }

    private void NotifyStateEnter() {
        string prevS = previousState.GetType().ToString();
        string activeS = currentState.GetType().ToString();
        IAiState state = GetComponent(prevS) as IAiState;
        state.OnStateEnter(prevS, activeS);
    }

    public void TriggerEnter2D(Collider2D my, Collider2D other) {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true ) {
            currentState.TriggerEnter(my, other);
        }
    }

    public void TriggerStay2D(Collider2D my, Collider2D other) {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true ) {
            currentState.TriggerStay(my, other);
        }
    }
    public void TriggerExit2D(Collider2D my, Collider2D other) {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true ) {
            currentState.TriggerExit(my, other);
        }
    }
}
