using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Vector3 Offset;
    public void SetHealth(int currentHealth, int maxHealth) {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;
    }
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
