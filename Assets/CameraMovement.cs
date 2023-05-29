using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    private float leftBarier;

    private void Start()
    {
        leftBarier = (13.75f * TrainManager.Instance.vagonsToAttack.Count) + 10;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveX * moveSpeed * Time.deltaTime, 0f, 0f);
        transform.Translate(movement);

        if (transform.position.x < 10)
        {
            transform.position = new Vector3(10,transform.position.y, transform.position.z);
        }
        else if(transform.position.x > leftBarier)
        {
            transform.position = new Vector3(leftBarier, transform.position.y, transform.position.z);
        }
    }
}
