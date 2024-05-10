using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    private int damage;
    public List<string> badAgainst;
    public float lifeTime;
    public float defaultSpeed;
    public float speed;
    public float speedUpOverTime;
    public float hitDistance;
    public float ballisticOffset;
    public float penetrationRatio;
    private Vector2 originPoint;
    private Transform target;
    private Vector2 aimPoint;
    private Vector2 myVirtualPosition;
    private Vector2 myPreviousPosition;
    private float counter;
    private SpriteRenderer sprite;

    public void SetDamage(int damage) {
        if (DataPersistenceManager.instance.projectileData != null) {
            this.damage = damage * (10 + DataPersistenceManager.instance.projectileData.bulletDamageScale) / 10;
        } else {
            this.damage = damage;
        }
    }
    public void Fire(Transform target) {
        AudioManager.instance.PlaySfx("Sniper");
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        originPoint = myVirtualPosition = myPreviousPosition = transform.position;
        this.target = target;
        aimPoint = GetPenetrationPoint(target.position);
    }

    void Update()
    {
        counter += Time.deltaTime;
        speed += Time.deltaTime * speedUpOverTime;
        if (target != null) {
            aimPoint = GetPenetrationPoint(target.position);
        }
        Vector2 originDistance = aimPoint - originPoint;
        Vector2 distanceToAim = aimPoint - (Vector2)myVirtualPosition;
        myVirtualPosition = Vector2.Lerp(originPoint, aimPoint, counter * speed / originDistance.magnitude);
        transform.position = myVirtualPosition;
        LookAtDirection2D((Vector2)transform.position - myPreviousPosition);
        myPreviousPosition = transform.position;
        sprite.enabled = true;
        if (distanceToAim.magnitude <= hitDistance) {
            counter = 0f;
            if (DataPersistenceManager.instance.projectileData != null) {
                speed = defaultSpeed * (10 + DataPersistenceManager.instance.projectileData.bulletSpeed) / 10;
            } else {
                speed = defaultSpeed;
            }
            gameObject.SetActive(false);
            transform.position = new Vector3(0,0,0);
        }
    }

    private void LookAtDirection2D(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    private Vector2 GetPenetrationPoint(Vector2 targetPosition) {
        Vector2 penetrationVector = targetPosition - originPoint;
        return originPoint + penetrationVector * (1f + penetrationRatio);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true) {
            DamageTaker damageTaker = other.GetComponent<DamageTaker>();
            if (damageTaker != null) {
                if (badAgainst.Contains(other.gameObject.name)) {
                    damageTaker.TakeDamage(damage / 2);
                } else {
                    damageTaker.TakeDamage(damage);
                }   
            }
        }
    }
}
