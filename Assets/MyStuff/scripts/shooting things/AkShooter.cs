using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkShooter : Shooter
{
    public int maxHealth = 100;
    private float currentHealth;

    public float damage, fireRate;
    public GameObject bulletPrefab;

    private float nextFireTime;
    [SerializeField]private Transform bulletSpawnPoint;

    private void Start()
    {
        currentHealth = maxHealth;
        nextFireTime = Time.time + 5;
    }

    public void Update()
    {
        if(Time.time >= nextFireTime)
        {
            Shoot(damage, fireRate, bulletSpawnPoint, bulletPrefab);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }
}
