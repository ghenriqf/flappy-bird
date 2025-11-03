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

    private bool _hasCollided = false;

    private GameManager gameManager;
    private GameManagerSecret gameManagerSecret;

    private void Start()
    {
        if (rigidBody2d == null)
            rigidBody2d = GetComponent<Rigidbody2D>();

        // tenta encontrar um dos dois game managers na cena
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
            gameManagerSecret = FindAnyObjectByType<GameManagerSecret>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rigidBody2d.linearVelocity.y * rotationSpeed);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2d.linearVelocity = Vector2.up * jumpSpeed;

            if (jumpSound != null)
                AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position, 0.6f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return;

        if (collision.gameObject.CompareTag("collision"))
        {
            _hasCollided = true;
            
            if (gameManager != null)
                gameManager.GameOver();
            else if (gameManagerSecret != null)
                gameManagerSecret.GameOver();

            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 0.6f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("portal"))
        {
            SceneManager.LoadScene("SecretScenes");
        }

        if (other.CompareTag("score"))
        {
            if (pointSound != null)
                AudioSource.PlayClipAtPoint(pointSound, Camera.main.transform.position, 0.6f);
            
            if (gameManager != null)
                gameManager.IncrementScore();
            else if (gameManagerSecret != null)
                gameManagerSecret.IncrementScore();
        }
    }
}