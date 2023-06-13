using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [HideInInspector]public float speed;
    public Vector3 smer;

    void Update()
    {
        speed = TruckManager.Instance.speedOfTime;

        if (!GameplayManager.instance.failed)
        {
            transform.Translate(smer * Time.deltaTime * speed);
        }

        if (transform.position.x > 100 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 40;
        }
        else
        {
            speed = 28;
        }*/
    }
}