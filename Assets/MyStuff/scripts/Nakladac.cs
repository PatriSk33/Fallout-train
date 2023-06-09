using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nakladac : MonoBehaviour
{
    // Transfer items
    [HideInInspector] public int inventory; 
    public int maxInv;
    public bool canTransfer = true;
    private bool startedTransfering;

    // Health not used right now
    private int health; 
    public int maxHealth;

    // Movement
    public Transform waypointPosition;
    [HideInInspector] public float movementSpeed;

    // Going Back
    private Vector3 GoAwayPoition;
    private bool canGoBack;


    private void Awake()
    {
        GoAwayPoition = transform.position;
    }

    private void Update()
    {
        movementSpeed = TruckManager.Instance.speedOfTime + 1;

        if (canGoBack)
        {
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.right * 2);
            if (transform.position.x >= GoAwayPoition.x)
            {
                SpawnerOfEnemies.Instance.SpawnNakladac();
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypointPosition.position, 0.1f);
        }
    }

    public void StartTransfer(TruckManager truck)
    {
        StartCoroutine(Transfer(truck));
    }

    IEnumerator Transfer(TruckManager truck)
    {
        while (canTransfer)
        {
            while (truck.inventory > 0 && inventory < maxInv)
            {
                truck.inventory--;
                inventory++;
                yield return new WaitForSeconds(2);
            }
            if (truck.inventory == 0 || inventory == maxInv)
            {
                GoBack();
            }
        }
    }

    public void GoBack()
    {
        //Stop transfer
        canTransfer = false;
        StopCoroutine("Transfer");

        //Dont get caught in any other vagon
        gameObject.GetComponent<BoxCollider>().enabled = false;

        //Go back to the starting postion
        canGoBack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Mathf.Abs(transform.position.x - waypointPosition.position.x) <= 0.1f)
        {
            if (other.CompareTag("Truck") && !startedTransfering)
            {
                StartTransfer(other.GetComponent<TruckManager>());
                startedTransfering = true;
            }
        }
    }
}
