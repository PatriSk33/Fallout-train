using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    private int leftLine, rightLine;
    private float z = 0;
    private int currentLine;

    [Tooltip("Speed of left and right movement")] public float jumpSpeed = 100;
    [Tooltip("Speed of forward and back movement")] public float maxSpeed = 12; // Maximum speed of the truck
    public float decelerationRate = 4f; // Rate at which the speed decreases
    public float accelerationRate = 2f; // Rate at which the speed increases
    private float currentSpeed = 2f; // Current speed of the truck
    private float normalDecelerationRate = 1; // Store the normal deceleration rate

    private bool isIncreasingSpeed = false;
    private bool isDecreasingSpeed = false;

    private void Start()
    {
        leftLine = -2;
        rightLine = 2;
        normalDecelerationRate = decelerationRate; // Store the initial deceleration rate
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isIncreasingSpeed = true;
            isDecreasingSpeed = false;
            decelerationRate = normalDecelerationRate; // Reset the deceleration rate when UpArrow is pressed
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isIncreasingSpeed = false;
            decelerationRate = normalDecelerationRate; // Reset the deceleration rate when UpArrow is released
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDecreasingSpeed = true;
            decelerationRate = normalDecelerationRate * 2f; // Double the deceleration rate when DownArrow is held
        }
        else
        {
            isDecreasingSpeed = false;
            decelerationRate = normalDecelerationRate; // Reset the deceleration rate when DownArrow is released
        }

        if (isIncreasingSpeed)
        {
            IncreaseSpeed();
        }
        else if (isDecreasingSpeed)
        {
            DecreaseSpeed();
        }
        else
        {
            SlowDecreaseSpeed();
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

    private void IncreaseSpeed()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, accelerationRate * Time.deltaTime);
        TruckManager.Instance.speedOfTime = currentSpeed;
    }

    private void DecreaseSpeed()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decelerationRate * Time.deltaTime);
        TruckManager.Instance.speedOfTime = currentSpeed;
    }

    private void SlowDecreaseSpeed()
    {
        if (currentSpeed > 0f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decelerationRate * 0.5f * Time.deltaTime);
            TruckManager.Instance.speedOfTime = currentSpeed;
        }
    }
}
