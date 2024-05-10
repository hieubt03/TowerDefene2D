using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : MonoBehaviour, IProjectile
{
    private int damage;
    public List<string> badAgainst;
    public float defaultSpeed = 5f;
    public float speed = 5f;
    public float speedUpOverTime = 0.05f;
    public float hitDistance = 0.2f;
    private float counter;
    private Vector2 originPoint;
    private Vector2 aimPoint;
    private Vector2 myVirtualPosition;
    private Transform target;
    private SpriteRenderer sprite;
    public float ballisticOffset = 0.5f;

    public void SetDamage(int damage) {
        if (DataPersistenceManager.instance.projectileData != null) {
            this.damage = damage * (10 + DataPersistenceManager.instance.projectileData.lightballDamageScale) / 10;
        } else {
            this.damage = damage;
        }
    }
    public void Fire(Transform target) {
        AudioManager.instance.PlaySfx("Lightning");
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        originPoint = myVirtualPosition = transform.position;
        this.target = target;
        aimPoint = target.position;
        
    }

    void Update() {
        counter += Time.deltaTime;
        speed += Time.deltaTime * speedUpOverTime;
        if (target != null) {
            aimPoint = target.position;
        }
        Vector2 originDistance = aimPoint - originPoint;
        Vector2 distanceToAim = aimPoint - (Vector2)myVirtualPosition;
        myVirtualPosition = Vector2.Lerp(originPoint, aimPoint, counter * speed / originDistance.magnitude);
        transform.position = myVirtualPosition;
        sprite.enabled = true;
        if (distanceToAim.magnitude <= hitDistance) {
            if (target != null) {
                DamageTaker damageTaker = target.GetComponent<DamageTaker>();
                if (damageTaker != null) {
                    if (badAgainst.Contains(target.name)) {
                        damageTaker.TakeDamage(damage / 2);
                    } else {
                        damageTaker.TakeDamage(damage);
                    }   
                }
            }
            counter = 0f;
            if (DataPersistenceManager.instance.projectileData != null) {
                speed = defaultSpeed * (10 + DataPersistenceManager.instance.projectileData.lightbalSpeed) / 10;
            } else {
                speed = defaultSpeed;
            }
            gameObject.SetActive(false);
            transform.position = new Vector3(0,0,0);
        }
    }
}
