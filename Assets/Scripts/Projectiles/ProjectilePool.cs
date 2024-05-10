using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public int poolSize;
    private List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] private GameObject poolPrefab;

    void Start() {
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(poolPrefab, gameObject.transform);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject getPoolObjects() {
        for (int i = 0; i < poolObjects.Count; i++) {
            if (!poolObjects[i].activeInHierarchy) {
                return poolObjects[i];
            }
        }
        return null;
    }
}
