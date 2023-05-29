using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform target = null;
    public bool isEnemy;

    private TrainManager train;
    private SpawnerOfEnemies spawner;

    private float startTime;

    private void Start()
    {
        train = TrainManager.Instance;
        spawner = SpawnerOfEnemies.Instance;
        startTime = Time.time + 4;
    }
    private void Update()
    {
        if (startTime < Time.time)
        {
            if (target != null)
            {
                transform.LookAt(target);
            }
            else
            {
                NewTarget();
            }
        }
    }

    public void NewTarget()
    {
        if (target == null)
        {
            if (isEnemy  && train.defensers.Count > 0)
            {
                target = train.defensers[Random.Range(0, train.defensers.Count)].transform;
            }
            else if (!isEnemy && spawner.enemiesOnField.Count > 0)
            {
                target = spawner.enemiesOnField[Random.Range(0, spawner.enemiesOnField.Count)].transform;
            }
        }
    }
}
