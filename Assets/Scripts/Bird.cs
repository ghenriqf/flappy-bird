using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2d;
    [SerializeField] private float jumpSpeed = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    private AudioSource _audioSource;
    
    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        rigidBody2d.linearVelocity = Vector2.up * jumpSpeed;
        
        if(_audioSource) _audioSource.Play();
        
    }
    
    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0,rigidBody2d.linearVelocity.y * rotationSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("collision"))
        {
            Die();
        }
    }

    private static void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
