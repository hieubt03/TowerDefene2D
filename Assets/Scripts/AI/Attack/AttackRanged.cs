using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackRanged : MonoBehaviour, IAttack
{
    public int damage = 1;
    public float cooldown = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float reloadSpeed;
    private Animator anim;
    private float cooldownCounter;
    private Transform currentTarget;
    public ProjectilePool projectilePool;

    void Awake() {
        if (DataPersistenceManager.instance.projectileData != null) {
            reloadSpeed = Time.deltaTime * (10 + DataPersistenceManager.instance.projectileData.arrowAttackSpeed) / 10;
        } else {
            reloadSpeed = Time.deltaTime;
        }
        anim = GetComponent<Animator>();
        cooldownCounter = cooldown;
        Debug.Assert(projectilePrefab && firePoint, "Wrong initial Parameter");
    }

    void Update() {
        if (cooldownCounter < cooldown) {
            cooldownCounter += reloadSpeed;
        }
    }
    public void Attack(Transform target) {
        if (cooldownCounter >= cooldown) {
            cooldownCounter = 0f;
            SetSpriteDirection(target);
            TriggetShoot(target);
            currentTarget = target;
        }
    }
    private void SetSpriteDirection(Transform target) {
        Vector2 direction = target.position - transform.position;
        if (direction.x > 0f && transform.localScale.x > 0f) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0f && transform.localScale.x < 0f) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    private void Fire() {
        if (currentTarget != null) {
            GameObject projectile = projectilePool.getPoolObjects();
            projectile.SetActive(true);
            projectile.transform.position = firePoint.position;
            IProjectile bullet = projectile.GetComponent<IProjectile>();
            bullet.SetDamage(damage);
            bullet.Fire(currentTarget);
        }
    }

    private void TriggetShoot(Transform target) {
        if (target != null) {
            Vector2 direction = (Vector2)target.position - (Vector2)firePoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (anim != null) {
                switch (angle) {
                case float n when n > -22.5 && n <= 22.5:
                    anim.SetTrigger("ShootSide");
                    break;
                case float n when n > 22.5 && n <= 67.5:
                    anim.SetTrigger("ShootTop01");
                    break;
                case float n when n > 67.5 && n <= 112.5:
                    anim.SetTrigger("ShootTop02");
                    break;
                case float n when n > 112.5 && n <= 157.5:
                    anim.SetTrigger("ShootTop01");
                    break;
                case float n when n > 157.5 || n <= -157.5:
                    anim.SetTrigger("ShootSide");
                    break;
                case float n when n > -157.5 && n <= -112.5:
                    anim.SetTrigger("ShootBot01");
                    break;
                case float n when n > -112.5 && n <= -67.5:
                    anim.SetTrigger("ShootBot02");
                    break;
                case float n when n > -67.5 && n <= -22.5:
                    anim.SetTrigger("ShootBot01");
                    break;
                default:
                    anim.SetTrigger("ShootSide");
                    break;
                }
            }
        }
    }
    
}
