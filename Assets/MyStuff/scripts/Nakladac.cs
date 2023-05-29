using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Nakladac : MonoBehaviour
{
    public int inventory, maxInv;
    public NavMeshAgent agent;
    int index;
    public bool canTransfer;

    public int health = 10, maxHealth = 10;

    //Death aka go away from screen
    public Transform GoAwayPoition;

    private void Start()
    {
        index = Random.Range(0, TrainManager.Instance.vagonsToAttack.Count);
        if (agent.enabled)
        {
            agent.destination = TrainManager.Instance.vagonsToAttack[index].transform.position;
        }
    }
    public void StartTransfer(vagon vagon)
    {
        StartCoroutine(Transfer(vagon));
    }
    IEnumerator Transfer(vagon vagon)
    {
        while (canTransfer)
        {
            while (vagon.inventory > 0 && inventory < maxInv)
            {
                vagon.inventory--;
                inventory++;
                yield return new WaitForSeconds(2);
            }
            if (vagon.inventory == 0 || inventory == maxInv)
            {
                GoBack();
            }
        }
    }

    public void GoBack()
    {
        canTransfer = false;
        agent.destination = GoAwayPoition.position;
        StopCoroutine("Transfer");
        Destroy(gameObject, 10);
        SpawnerOfEnemies.Instance.SpawnNakladac();
    }
}
