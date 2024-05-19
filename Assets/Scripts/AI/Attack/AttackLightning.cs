using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackLightning : MonoBehaviour, IAttack
{
    public int damage;
    public float cooldown = 0.1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float cooldownCounter;
    public float reloadSpeed;
    public ProjectilePool projectilePool;
    void Awake() {
        if (DataPersistenceManager.instance.gameData != null) {
            reloadSpeed = reloadSpeed * (10 + DataPersistenceManager.instance.gameData.lightballAttackSpeed) / 10;
        }
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
            Fire(target);
        }
    }
    private void Fire(Transform target) {
        if (target != null) {
            GameObject projectile = projectilePool.getPoolObjects();
            projectile.SetActive(true);
            projectile.transform.position = firePoint.position;
            IProjectile bullet = projectile.GetComponent<IProjectile>();
            bullet.SetDamage(damage);
            bullet.Fire(target);
        }
    }
}
