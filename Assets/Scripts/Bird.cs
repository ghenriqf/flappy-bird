using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2d;
    [SerializeField] private float jumpSpeed = 5f;
    
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        rigidBody2d.linearVelocity = Vector2.up * jumpSpeed;
    }
    
    void Update()
    {
        Jump();
    }
}
