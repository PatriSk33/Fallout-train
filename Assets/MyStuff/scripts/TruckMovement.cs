using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    private int leftLine, rightLine;
    private float z = 0;
    private int currentLine;

    [Tooltip("Speed of left and right movement")] public float jumpSpeed = 100;
    [Tooltip("Speed of forward and back movement")]public float speed = 12;

    private void Start()
    {
        leftLine = -2;
        rightLine = 2;
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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBackward();
        }
        else
        {
            TruckManager.Instance.speedOfTime = 10;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, z), Time.deltaTime * jumpSpeed);
    }

    public void MoveLeft()
    {
        if (currentLine != leftLine)
        {
            currentLine--;
            z -= 7f;
        }
    }
    public void MoveRight()
    {
        if (currentLine != rightLine)
        {
            currentLine++;
            z += 7f;
        }
    }

    public void MoveForward()
    {
        //transform.Translate(Vector3.left * speed * Time.deltaTime);
        TruckManager.Instance.speedOfTime = speed;
    }
    public void MoveBackward()
    {
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
        TruckManager.Instance.speedOfTime = 5;
    }
}
