using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float radius = 0.3f;
    private int damage;
    public List<string> badAgainst;
    public GameObject explosion;
    public float explosionDuration = 1f;
    private bool isQuitting;
    private bool isInitiate = true;

    public void SetDamage(int damage) {
        if (DataPersistenceManager.instance.gameData != null) {
            this.damage = damage * (10 + DataPersistenceManager.instance.gameData.bombDamageScale) / 10;
        } else {
            this.damage = damage;
        }
    }
    void OnEnable() {
        EventManager.StartListening("SceneQuit", SceneQuit);
    }

    void OnDisable() {
        EventManager.StopListening("SceneQuit", SceneQuit);
        if (isQuitting == false) {
            AudioManager.instance.PlaySfx("Explosion");
            // Find all colliders in specified radius
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in cols) {
                if (LevelManager.IsCollisionValid(gameObject.tag, col.gameObject.tag) == true) {
                    DamageTaker damageTaker = col.gameObject.GetComponent<DamageTaker>();
                    if (damageTaker != null) {
                        if (badAgainst.Contains(col.gameObject.name)) {
                            damageTaker.TakeDamage(damage / 2);
                        } else {
                            damageTaker.TakeDamage(damage);
                        }   
                    }
                }
            }
            if (explosion != null && isInitiate == false) {
                Destroy(Instantiate<GameObject>(explosion, transform.position, transform.rotation), explosionDuration);
            }
            isInitiate = false;
        }
    }

    void OnApplicationQuit() {
        isQuitting = true;
    }

    private void SceneQuit(GameObject obj, string param) {
        isQuitting = true;
    }
}
