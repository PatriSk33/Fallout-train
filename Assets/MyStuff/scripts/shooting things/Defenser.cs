using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defenser : Shooter
{
    public float startShootingTime;

    public int maxHealth = 100;
    private float currentHealth;
    public Slider healthBar;

    [Range(0, 1)]public float criticalChange;
    public float criticalMultiplier;
    public float damage, fireRate;
    public int maxAmmo;
    private int currentAmmo;
    private bool isReloading;
    public float reloadTime;

    public GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    private float nextFireTime;

    private DraggableItem dragItem;

    public LookAt lookAt;

    private void Start()
    {
        dragItem = GetComponent<DraggableItem>();

        //Health
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;

        // Ammo
        currentAmmo = maxAmmo;
        nextFireTime = Time.time + startShootingTime;
    }

    public void Update()
    {
        if (Time.time >= nextFireTime && !GameplayManager.instance.end && lookAt.target != null)
        {
            if (SpawnerOfEnemies.Instance.enemiesOnField.Count > 0 && !dragItem.isDragging)
            {
                if (currentAmmo > 0)
                {
                    Shoot(damage, bulletSpawnPoint, bulletPrefab, criticalChange, criticalMultiplier, lookAt.transform);
                    currentAmmo--;
                }
                else if (!isReloading)
                {
                    StartCoroutine(Reload());
                }
            }
            nextFireTime = Time.time + fireRate;
        }


        // Health bar
        healthBar.value = currentHealth;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
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
