using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float damage;
    [SerializeField]private float speed = 10f;
    private float pociatok;
    [SerializeField] private float killTime;

    private void OnEnable()
    {
        pociatok = Time.time + killTime;
    }
    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Time.time > pociatok)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public void Fire()
    {
        // Activate any additional effects or behaviors upon firing the bullet
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for collision with other objects
        // Apply damage if necessary and handle any desired effects

        // Example: Destroy the bullet on collision
        if (other.CompareTag("Defenser"))
        {
            other.GetComponent<Defenser>().TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Attacker"))
        {
            other.GetComponent<AttackerShoot>().TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Driver"))
        {
            SpawnerOfEnemies.Instance.driversOnField.Remove(other.gameObject);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
