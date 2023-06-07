using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [HideInInspector]public float speed;
    [SerializeField]private TrainManager playerCS;
    public Vector3 smer;

    private void Start()
    {
        speed = TrainManager.Instance.speed;
    }
    void Update()
    {
        if (playerCS.failed == false)
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