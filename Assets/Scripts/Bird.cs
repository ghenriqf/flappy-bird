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

    private GameManager gameManager; // Referência ao GameManager principal
    private GameManagerSecret gameManagerSecret; // Referência ao GameManager secreto

    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        gameManagerSecret = FindAnyObjectByType<GameManagerSecret>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        // é uma função estática que converte um ângulo de rotação no formato de Ângulos de Euler (um vetor com valores de rotação em torno dos eixos X, Y e Z) em um objeto
        transform.rotation = Quaternion.Euler(0, 0, rigidBody2d.linearVelocity.y * rotationSpeed);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2d.linearVelocity = Vector2.up * jumpSpeed;
            AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position, 0.6f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return; // Ignora colisões após a primeira

        // Verifica se colidiu com algo marcado como "collision"
        if (collision.gameObject.CompareTag("collision"))
        {
            _hasCollided = true; // Marca que colidiu

            // Chama GameOver no GameManager apropriado
            if (gameManager != null)
                gameManager.GameOver();
            else if (gameManagerSecret != null)
                gameManagerSecret.GameOver();

            // Toca som de colisão, se houver
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 0.6f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se entrar em um portal, muda para a cena secreta
        if (other.CompareTag("portal"))
        {
            SceneManager.LoadScene("SecretScenes");
        }

        // Se entrar em uma área de pontuação
        if (other.CompareTag("score"))
        {
            AudioSource.PlayClipAtPoint(pointSound, Camera.main.transform.position, 0.6f);
            
            // Incrementa pontuação no GameManager apropriado
            if (gameManager != null)
                gameManager.IncrementScore();
            else if (gameManagerSecret != null)
                gameManagerSecret.IncrementScore();
        }
    }
}