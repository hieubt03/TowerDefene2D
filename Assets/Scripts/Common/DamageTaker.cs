using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float hitDisplayTime;
    private SpriteRenderer sprite;
    private bool hitCoroutine;
    private HealthBar healthBar;
    private Animator anim;
    private Collider2D bodyCollider;
    void Awake() {
        healthBar = GetComponentInChildren<HealthBar>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth, maxHealth);
        Debug.Assert(healthBar && sprite && anim, "Wrong initial");
    }

    public void TakeDamage(int damage) {
        if (currentHealth >= damage) {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth, maxHealth);
            if (hitCoroutine == false) {
                StartCoroutine(DisplayAttacked());
            }
        } else {
            currentHealth = 0;
            healthBar.gameObject.SetActive(false);
            NavAgent navAgent = GetComponent<NavAgent>();
            navAgent.move = false;
            anim.SetTrigger("Dead");
            bodyCollider.enabled = false;
        }
    }

    public void Dead() {   
        EventManager.TriggerEvent("ZombieDead", gameObject, null);
        AudioManager.instance.PlaySfx("ZombieDead");
        Destroy(gameObject);
    }
    
    IEnumerator DisplayAttacked() {
        hitCoroutine = true;
        Color originColor = sprite.color;
        float counter;
        for (counter = 0f; counter < hitDisplayTime; counter += Time.deltaTime) {
            sprite.color = Color.Lerp(originColor, Color.black, Mathf.PingPong(counter, hitDisplayTime));
            yield return new WaitForEndOfFrame();
        }
        sprite.color = originColor;
        hitCoroutine = false;
    }
}
