using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : Shooter
{
    public int maxHealth = 100;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            SpawnerOfEnemies.Instance.driversOnField.Remove(gameObject);
            Destroy(gameObject, 1);
        }
    }
}
