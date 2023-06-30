using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    void Update()
    {
        speed = TruckManager.Instance.speedOfTime;

        if (!GameplayManager.instance.failed)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }

        if (transform.position.x > 100 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}