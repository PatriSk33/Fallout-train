using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class vagon : MonoBehaviour
{
    public int inventory = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nakladac"))
        {
            other.GetComponent<Nakladac>().canTransfer = true;
            other.GetComponent<Nakladac>().StartTransfer(this);
        }
    }

    private void Update()
    {
        if(inventory <= 0)
        {
            TrainManager.Instance.vagonsToAttack.Remove(this);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
