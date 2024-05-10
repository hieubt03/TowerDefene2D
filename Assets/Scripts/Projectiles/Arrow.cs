using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Arrow : MonoBehaviour, IProjectile
{
    private int damage;
    public List<string> badAgainst;
    public float defaultSpeed;
    public float speed;
    public float speedUpOverTime;
    public float hitDistance;
    private float counter;
    private Vector2 originPoint;
    private Vector2 aimPoint;
    private Vector2 myVirtualPosition;
    private Vector2 myPreviousPosition;
    private Transform target;
    private SpriteRenderer sprite;
    public float ballisticOffset;
    public bool freezeRotation = false;

    public void SetDamage(int damage) {
        if (DataPersistenceManager.instance.projectileData != null) {
            this.damage = damage * (10 + DataPersistenceManager.instance.projectileData.arrowDamageScale) / 10;
        } else {
            this.damage = damage;
        }
    }
    public void Fire(Transform target) {
        AudioManager.instance.PlaySfx("Arrow");
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        originPoint = myVirtualPosition = myPreviousPosition = transform.position;
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
        transform.position = AddBallisticOffset(originDistance.magnitude, distanceToAim.magnitude);
        LookAtDirection2D((Vector2)transform.position - myPreviousPosition);
        myPreviousPosition = transform.position;
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
                speed = defaultSpeed * (10 + DataPersistenceManager.instance.projectileData.arrowSpeed) / 10;
            } else {
                speed = defaultSpeed;
            }
            gameObject.SetActive(false);
            transform.position = new Vector3(0,0,0);
        }
    }
    private void LookAtDirection2D(Vector2 direction) {
        if (freezeRotation == false) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
    }

    private Vector2 AddBallisticOffset(float originDistance, float distanceToAim) {
        if (ballisticOffset > 0f) {
            float offset = Mathf.Sin(Mathf.PI * ((originDistance - distanceToAim) / originDistance));
            offset *= originDistance;
            return (Vector2)myVirtualPosition + (ballisticOffset * offset * Vector2.up);
        }
        else {
            return myVirtualPosition;
        }
    }
}