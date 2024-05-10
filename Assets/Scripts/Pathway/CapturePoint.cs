using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    private bool isCaptured;
    
    void OnTriggerEnter2D(Collider2D other) {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true) {
            if (isCaptured == false) {
                isCaptured = true;   
            }
            EventManager.TriggerEvent("Captured", other.gameObject, null);
        }    
    }
}
