using UnityEngine;

public class BulletScript : MonoBehaviour
{   
    //Start Position
    private float pociatok;
    private Vector3 startPos;

    //Stats
    private float damage;

    [Header("Basic Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField][Tooltip("Time after which the bullet deseaper")] private float flyTime = 3;

    [Header("Sniper Bullet Stats")]
    public bool isSniperBullet;
    [Tooltip("For Sniper bullet")]public float longDistance, closeDistance;
    [Tooltip("Damage for each sniper distance")]public float longDistanceDamage, mediumDistanceDamage, closeDistanceDamage;

    private void OnEnable()
    {
        pociatok = Time.time + flyTime;
        startPos = transform.position;
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

        if (isSniperBullet)
        {
            if (other.CompareTag("Driver"))
            {
                if (Vector3.Distance(startPos, other.transform.position) > longDistance)
                {
                    other.GetComponent<Driver>().TakeDamage(longDistanceDamage);
                }
                else if (Vector3.Distance(startPos, other.transform.position) > closeDistance)
                {
                    other.GetComponent<Driver>().TakeDamage(mediumDistanceDamage);
                }
                else if (Vector3.Distance(startPos, other.transform.position) < closeDistance)
                {
                    other.GetComponent<Driver>().TakeDamage(closeDistanceDamage);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
