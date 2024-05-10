using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    [System.Serializable]
    public class Wave {
        public float  timeBeforeWave;
        public List<GameObject> zombies;
    }
    public float zombieSpawnDelay = 0.8f;
    public List<Wave> waves;
    public Wave nextWave;
    public float counter;
    public bool waveInProgress;
    public List<GameObject> zombiePrefabs;
    public List<GameObject> activeZombies = new List<GameObject>();
    private SpawnPoint[] spawnPoints;
    void Awake() {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Debug.Assert((spawnPoints != null) && (zombiePrefabs != null), "Wrong initial");
    }
    void Start() {
        if (waves.Count > 0) {
            nextWave = waves[0];
        }
    }
    void OnEnable() {
        EventManager.StartListening("ZombieDead", ZombieDead);
    }
    void OnDisable() {
        EventManager.StopListening("ZombieDead",ZombieDead);
    }
    void Update() {
        if ((waves.IndexOf(nextWave) >= 0) && (waveInProgress == false) && (activeZombies.Count <= 0)) {
            counter += Time.deltaTime;
            if (counter >= nextWave.timeBeforeWave) {
                counter = 0f;        
                StartCoroutine(SpawnWave());
                EventManager.TriggerEvent("WaveComplete", null, null);
            }
        }
        if ((waves.IndexOf(nextWave) < 0) && (activeZombies.Count <= 0)) {
            EventManager.TriggerEvent("AllZombiesAreDead", null, null);
            enabled = false;
        }
    }
    public void GetNextWave() {
        int index = waves.IndexOf(nextWave) +1;
        if (index < waves.Count) {
            nextWave = waves[index];
        } else {
            nextWave = null;
        }
    }
    private void ZombieDead(GameObject gameObject, string param) {
        if (activeZombies.Contains(gameObject) == true) {
            activeZombies.Remove(gameObject);
        }
    }
    private IEnumerator SpawnWave() {
        zombieSpawnDelay = Random.Range(0, 1f);
        waveInProgress = true;
        foreach (GameObject zombie in nextWave.zombies) {
            GameObject prefab = null;
            if (zombie != null) {
                prefab = zombie;
            } else {
                prefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Count)];
            }
            SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPoint.SpawnZombie(prefab);
            yield return new WaitForSeconds(zombieSpawnDelay);
        }
        GetNextWave(); 
        waveInProgress = false;
    }
}
