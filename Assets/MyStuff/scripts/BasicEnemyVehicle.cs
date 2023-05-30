using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyVehicle : MonoBehaviour
{
    // Movement
    [HideInInspector]public int indexOfLine;
    public Transform destination;
    public float movementSpeed;
    private bool gettingToPosition = true;

    // Death
    private Vector3 startPosition; // Backing positon
    private bool isBacking;
    public int spawnpointIndex;

    // Health
    private float health;
    public float maxHealth;

    // Attackers on the vehicle
    public List<GameObject> enemiesOnVehicle;

    private void Awake()
    {
        // Set stats
        health = maxHealth;
        startPosition = transform.position;

        // Put all attackers into the list of people on vehicle
        for (int i = 1; i < transform.childCount; i++)
        {
            enemiesOnVehicle.Add(transform.GetChild(i).gameObject);
        }
    }

    public void Update()
    {
        // Movement
        if (Mathf.Abs(transform.position.x - destination.position.x) > 0.1f && gettingToPosition)
        {
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.left);
        }
        else if (Mathf.Abs(transform.position.x - destination.position.x) <= 0.1f)
        {
            gettingToPosition = false;
            //Activate an Animation when the car moves back and forth
        }


        // Death because of vehice not having health
        if (health <= 0)
        {
            Death(true);
        }

        // Death because there is no one to drive the vehicle
        if(enemiesOnVehicle.Count == 0)
        {
            Death(false);
        }

        if(isBacking)
        {
            transform.Translate(Time.deltaTime * 30 * Vector3.right);
            if(transform.position.x >= startPosition.x) {
                Destroy(gameObject);
                SpawnerOfEnemies.Instance.vehicleOnField.Remove(gameObject);
            }
        }
    }

    public void Death(bool isExplosion)
    {
        // Removing vehicle from list
        SpawnerOfEnemies.Instance.RemoveVehicle(gameObject);

        if (isExplosion)
        {
            // Explosion
            Destroy(gameObject);
            SpawnerOfEnemies.Instance.vehicleOnField.Remove(gameObject);
            Debug.Log("Health of vehicle is at 0 OR bum bum = Explosion");
        }
        else
        {
            //Cuvaj a ak hitnes do niecoho tak explsion aj teba aj toho auta za tebou
            isBacking = true;
            Debug.Log("is Backing - no passagers");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            //Bum Bum = explosion of both vehicles
            Death(true);
            other.GetComponent<BasicEnemyVehicle>().Death(true);
        }
    }
}
