using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveComplete : MonoBehaviour
{
    private bool isInitiate = true;

    void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        EventManager.StopListening("WaveComplete", WaveCompletePopup);
    }
    private void OnDisable() {
        EventManager.StartListening("WaveComplete", WaveCompletePopup);
    }
    private void WaveCompletePopup(GameObject gameObj, string param) {
        if (isInitiate == false) {
            gameObject.SetActive(true);
            AudioManager.instance.PlaySfx("WaveComplete");
        }
        isInitiate = false;
    }
    
    private void SetActiveToFalse() {
         gameObject.SetActive(false);
    }   
}
