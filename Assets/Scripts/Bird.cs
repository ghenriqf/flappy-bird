using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2d;
    [SerializeField] private float jumpSpeed = 2f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Sons")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip pointSound;

    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2d.linearVelocity = Vector2.up * jumpSpeed;

            if (jumpSound != null)
            {
                AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position, 0.6f);
            }
        }
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rigidBody2d.linearVelocity.y * rotationSpeed);
    }

    private bool _hasCollided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return;

        if (collision.gameObject.CompareTag("collision"))
        {
            _hasCollided = true;

            var gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null)
                gameManager.GameOver();

            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 0.6f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("score"))
        {
            if (pointSound != null)
            {
                AudioSource.PlayClipAtPoint(pointSound, Camera.main.transform.position, 0.6f);
            }
            FindAnyObjectByType<GameManager>().IncrementScore();
        }
    }
}
