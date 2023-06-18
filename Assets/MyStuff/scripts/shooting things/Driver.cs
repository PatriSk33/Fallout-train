using UnityEngine;
using UnityEngine.UI;

public class Driver : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    public Slider healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    private void Update()
    {
        // Health bar
        healthBar.value = currentHealth;
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
