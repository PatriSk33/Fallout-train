using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float leftBarrier;
    private float rightBarrier = 10f;
    private float x = 10;
    private int currentVagon;
    private int vagonsCount;

    private void Start()
    {
        vagonsCount = TrainManager.Instance.vagonsToAttack.Count;
        leftBarrier = (18f * (vagonsCount - 1)) + 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, transform.position.y, transform.position.z), Time.deltaTime * 100);
    }

    public void MoveLeft()
    {
        if (x == leftBarrier)
        {
            currentVagon = 0;
            x = rightBarrier;
        }
        else if (currentVagon != vagonsCount)
        {
            x += 18f;
            currentVagon++;
        }
    }
    public void MoveRight()
    {
        if (x == rightBarrier)
        {
            currentVagon = vagonsCount;
            x = leftBarrier;
        }
        else if (currentVagon != 0)
        {
            currentVagon--;
            x -= 18f;
        }
    }
}
