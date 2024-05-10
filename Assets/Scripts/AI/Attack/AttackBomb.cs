using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackBomb : MonoBehaviour, IAttack
{
    public int damage;
    //Time wait to attack
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
            reloadSpeed = Time.deltaTime * (10 + DataPersistenceManager.instance.projectileData.bombAttackSpeed) / 10;
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
            currentTarget = target;
            if (anim != null) {
                anim.SetTrigger("Attack");
            }
        }
    }
    private void Fire() {
        if (currentTarget != null) {
            GameObject projectile = projectilePool.getPoolObjects();
            projectile.SetActive(true);
            projectile.transform.position = firePoint.position;
            IProjectile bullet = projectile.GetComponent<IProjectile>();
            Bomb bomb = projectile.GetComponent<Bomb>();
            bomb.SetDamage(damage);
            bullet.Fire(currentTarget);
        }
    }
}
