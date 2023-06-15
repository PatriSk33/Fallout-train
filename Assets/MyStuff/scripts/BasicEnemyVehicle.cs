using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyVehicle : MonoBehaviour
{
    // Movement
    [Header("Basic Stats")]
    [HideInInspector] public int indexOfLine;
    public Transform destination;
    [HideInInspector] private float movementSpeed = 8;
    public float turnSpeed = 30f;
    //private bool gettingToPosition = true;

    // Death
    private Vector3 startPosition; // Backing positon
    private bool isBacking, isGoingAway;
    public int spawnpointIndex;

    // Health
    [Header("Health")]
    private float health;
    public float maxHealth;

    // Driver
    [Header("Driver")]
    public bool driver = true;

    // Passangers
    [HideInInspector]public int enemiesOnVehicle;

    // Dodging
    [Header("Dodging")]
    [Range(0, 1)][SerializeField] private float changeOfDodging;
    public float dodgeDistance = 1.0f; // Distance to dodge
    public float dodgeDuration = 1.0f; // Duration of each dodge
    private Vector3 originalPosition; // Original position of the vehicle

    private void Awake()
    {
        // Set stats
        health = maxHealth;
        startPosition = transform.position;
    }

    public void Update()
    {
        // Movement
        if (!isBacking && !isGoingAway)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, Time.deltaTime * movementSpeed);
        }
        

        // Death because of vehice not having health
        if (health <= 0)
        {
            Death(true);
        }

        // Death because there is no one to drive the vehicle
        if (transform.GetChild(1) != null)
        {
            if (transform.GetChild(1).CompareTag("Driver")) {
                driver = true;
            }
            else
            {
                Death(false);
            }
        }


        if (isBacking)
        {
            // Making go backward
            transform.Translate(Time.deltaTime * (movementSpeed + 2) * Vector3.right);

            // Destroy it
            if (transform.position.x >= startPosition.x) {
                SpawnerOfEnemies.Instance.vehicleOnField.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        if (isGoingAway)
        {
            // Rotate the car to the left gradually
            if (Mathf.Abs(transform.eulerAngles.y) < 179f || Mathf.Abs(transform.eulerAngles.y) > 181f)
            {
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                transform.Translate(Time.deltaTime * (movementSpeed * 2) * Vector3.right, Space.World);

            }
            else
            {
                // Make it go Forward
                transform.Translate(Time.deltaTime * (movementSpeed * 2) * Vector3.left);

                //Destroy it
                if (transform.position.x >= startPosition.x)
                {
                    SpawnerOfEnemies.Instance.driversOnField.Remove(transform.GetChild(1).gameObject);
                    SpawnerOfEnemies.Instance.vehicleOnField.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
        }

        if (enemiesOnVehicle == 0)
        {
            Death(false);
        }
    }

    public void Death(bool isExplosion)
    {
        // Removing vehicle from list
        SpawnerOfEnemies.Instance.RemoveVehicle(gameObject);

        if (isExplosion)
        {
            // Explosion
            if (transform.GetChild(1) != null && transform.GetChild(1).CompareTag("Driver"))
            {
                SpawnerOfEnemies.Instance.driversOnField.Remove(transform.GetChild(1).gameObject);
            }
            SpawnerOfEnemies.Instance.vehicleOnField.Remove(gameObject);
            Debug.Log("Health of vehicle is at 0 OR bum bum = Explosion");
            Destroy(gameObject, 1.5f);
        }
        else
        {
            //Cuvaj a ak hitnes do niecoho tak explsion aj teba aj toho auta za tebou
            if (!driver)
            {
                isBacking = true;
                Debug.Log("is Backing - no Driver");
            }
            else if (enemiesOnVehicle == 0)
            {
                Debug.Log("getting away - no passangers");
                isGoingAway = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            if (Random.value > changeOfDodging)
            {
                // Vehicle dodging the other vehicle
                originalPosition = transform.position;

                // Randomly determine the dodge direction
                float direction = Random.value < 0.5f ? -1f : 1f;

                StartCoroutine(Dodge(direction * dodgeDistance));
            }
            else
            {
                // Explosion of both vehicles
                other.GetComponent<BasicEnemyVehicle>().Death(true);
                Death(true);
            }
            
        }
        else if (other.CompareTag("Obstacle"))
        {
            if(Random.value < changeOfDodging)
            {
                // Vehicle dodging the obstacle
                originalPosition = transform.position;

                // Randomly determine the dodge direction
                float direction = Random.value < 0.5f ? -1f : 1f;

                StartCoroutine(Dodge(direction * dodgeDistance));
            }
            else
            {
                // Missed
                Death(true);
            }
        }
    }

    private IEnumerator Dodge(float offset)
    {
        float startTime = Time.time;
        Vector3 targetPosition = originalPosition + transform.forward * offset;
        Vector3 returnPosition = transform.position; // Store the current position as the return position

        float halfDodgeDuration = dodgeDuration / 2f; // Calculate the half dodge duration

        while (Time.time < startTime + halfDodgeDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (Time.time - startTime) / dodgeDuration);
            yield return null;
        }

        startTime = Time.time; // Reset the start time for the return phase

        while (Time.time < startTime + halfDodgeDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, returnPosition, (Time.time - startTime) / dodgeDuration);
            yield return null;
        }

        transform.position = originalPosition; // Set the position to the original position
    }
}
