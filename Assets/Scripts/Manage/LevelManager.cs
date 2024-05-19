using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private UiManager uiManager;
    [SerializeField] private string levelSelectId;
    private int spawnNumbers;

    void Awake() {
        uiManager = FindObjectOfType<UiManager>();
        spawnNumbers = FindObjectsOfType<SpawnPoint>().Length;
        DataPersistenceManager.instance.ChangeSelectedProfileId(levelSelectId);
        if (spawnNumbers <= 0) {
            Debug.LogError("Have no spawners");
        }
        Debug.Assert(uiManager, "Wrong initial parameters");
    }

    void OnEnable() {
        EventManager.StartListening("Captured", Captured);
        EventManager.StartListening("AllZombiesAreDead", AllZombiesAreDead);
        EventManager.StartListening("WaveComplete", WaveComplete);
    }

    void OnDisable() {
        EventManager.StopListening("Captured", Captured);
        EventManager.StopListening("AllZombiesAreDead", AllZombiesAreDead);
        EventManager.StopListening("WaveComplete", WaveComplete);
    }

    public static bool IsCollisionValid(string myTag, string otherTag) {
        bool res = false;
        switch (myTag) {
            case "Tower":
            case "Defender":
                switch (otherTag) {
                    case "Zombie":
                        res = true;
                        break;
                }
                break;
            case "Zombie":
                switch (otherTag) {
                    case "Defender":
                    case "Tower":
                        res = true;
                        break;
                }
                break;
            case "Projectile":
                switch (otherTag) {
                    case "Zombie":
                        res = true;
                        break;
                }
                break;
            case "CapturePoint":
                switch (otherTag) {
                    case "Zombie":
                        res = true;
                        break;
                }
                break;
            default:
                Debug.Log("Unknown collision tag => " + myTag + " - " + otherTag);
                break;
        }
        return res;
    }
    private void Captured(GameObject gameObj, string param) {
        uiManager.LoseHealthPoint();
        EventManager.TriggerEvent("ZombieDead", gameObj, null);
        Destroy(gameObj);
    }
    private void WaveComplete(GameObject gameObj, string param) {
        int currentWave = uiManager.GetWaveNumber();
        uiManager.SetWave(currentWave += 1);
    }
    private void AllZombiesAreDead(GameObject gameObj, string param) {
        uiManager.GoToVictoryMenu();
    }    
}
