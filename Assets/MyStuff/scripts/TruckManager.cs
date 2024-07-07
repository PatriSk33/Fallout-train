using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public static TruckManager Instance;

    //Defensers
    public List<GameObject> defensers;

    //Stats
    public float speedOfTime;
    [Tooltip("Amount of items in the truck")]public int inventory;
    public float maxHealth;

    private float health;

    private void Awake()
    {
       Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        //Get defensers
    }

    public void RemoveDefender(GameObject defender)
    {
        defensers.Remove(defender);

        if (defensers.Count == 0)
        {
            GameplayManager.instance.Lost();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameplayManager.instance.Lost();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameplayManager.instance.Lost();
        }
        else if (other.CompareTag("Vehicle"))
        {
            health--;
            other.GetComponent<BasicEnemyVehicle>().DecreaseHealth(2);
            other.GetComponent<BasicEnemyVehicle>().GotHitByTruck();
        }
    }
}
