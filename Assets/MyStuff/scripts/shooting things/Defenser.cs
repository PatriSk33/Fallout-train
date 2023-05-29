using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defenser : Shooter
{
    public int maxHealth = 100;
    private float currentHealth;

    public float damage, fireRate;
    public GameObject bulletPrefab;

    private float nextFireTime;
    [SerializeField]private Transform bulletSpawnPoint;

    private DraggableItem dragItem;

    private void Start()
    {
        dragItem = GetComponent<DraggableItem>();
        currentHealth = maxHealth;
        nextFireTime = Time.time + 5;
    }

    public void Update()
    {
        if (Time.time >= nextFireTime && !GameplayManager.instance.end)
        {
            if (SpawnerOfEnemies.Instance.enemiesOnField.Count > 0 && !dragItem.isDragging)
            {
                Shoot(damage, fireRate, bulletSpawnPoint, bulletPrefab);
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death(false, gameObject);
        }
    }
}
