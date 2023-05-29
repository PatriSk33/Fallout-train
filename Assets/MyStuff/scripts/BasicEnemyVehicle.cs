using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyVehicle : MonoBehaviour
{
    [HideInInspector]public Transform destination;
    public NavMeshAgent agent;
    private float health;
    public float maxHealth = 20;
    public List<GameObject> enemiesOnVehicle;

    private void Start()
    {
        health = maxHealth;
        // Iterate through each child object
        for (int i = 1; i < transform.childCount; i++)
        {
            enemiesOnVehicle.Add(transform.GetChild(i).gameObject);
        }
    }
    public void Update()
    {
        //Movement
        if (agent.destination != destination.position && enemiesOnVehicle.Count > 0)
        {
            agent.destination = destination.position;
        }
        else
        {
            // Calculate the direction to the destination
            Vector3 direction = destination.position - transform.position;
            direction.y = 0; // Optionally, set the y component to 0 to ignore vertical difference

            if (direction != Vector3.zero)
            {
                // Rotate the agent to face the destination
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        //Death
        if (health <= 0)
        {
            Death();
        }

        if(enemiesOnVehicle.Count == 0)
        {
            transform.rotation = Quaternion.identity;
            agent.destination = transform.position + new Vector3(30,0,0);
            agent.speed = 200;
            Death();
            Destroy(gameObject, 10);
        }
    }

    void Death()
    {
        SpawnerOfEnemies.Instance.vehicleOnField.Remove(gameObject);
        if (SpawnerOfEnemies.Instance.vehicleOnField.Count == 0)
        {
            SpawnerOfEnemies.Instance.canSpawn = true;
        }
        Destroy(agent.gameObject);
    }
}
