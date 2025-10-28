using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField]  private float speed = 0.75f;
    private void Start()
    {
        
    }

    private void Move()
    {
        transform.position +=  Vector3.left * (speed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
        
    }
}
