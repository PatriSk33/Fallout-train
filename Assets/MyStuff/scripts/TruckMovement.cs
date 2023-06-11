using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    private int leftLine, rightLine;
    private float z = 0;
    private int currentLine;
    private int linesCount;

    public float jumpSpeed = 100;

    private void Start()
    {
        leftLine = -2;
        rightLine = 2;
        linesCount = Mathf.Abs(leftLine) + rightLine + 1;
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
}
