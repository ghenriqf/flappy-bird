using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField]  private float speed = 0.65f;
    void Start()
    {
        
    }

    void Move()
    {
        transform.position +=  Vector3.left * (speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
        
    }
}
