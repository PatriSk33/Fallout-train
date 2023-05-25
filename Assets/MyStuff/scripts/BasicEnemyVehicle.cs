using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyVehicle : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;
    private float health;
    public float maxHealth = 20;

    private void Start()
    {
        health = maxHealth;
    }
    public void Update()
    {
        //Movement
        agent.destination = destination.position; 

        //Death
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        SpawnerOfEnemies.Instance.enemiesOnField.Remove(gameObject);
        SpawnerOfEnemies.Instance.canSpawn = true;
        Destroy(agent.gameObject);
    }
}
