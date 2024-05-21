using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Pathway pathway;
    private WaveManager waveManager;

    void Awake() {
        pathway = GetComponentInParent<Pathway>();
        waveManager = GetComponentInParent<WaveManager>();
        Debug.Assert((pathway != null) && (waveManager != null), "Wrong initial");
       
    }

    public void SpawnZombie(GameObject prefab) {
        GameObject newZombie = Instantiate(prefab, transform.position, transform.rotation);
        newZombie.GetComponent<AiStateMove>().pathway = pathway;
        NavAgent navAgent = newZombie.GetComponent<NavAgent>();
        navAgent.move = true;
        waveManager.activeZombies.Add(newZombie);
    }
}
