using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject getReadySprite;
    public GameObject bird;
    public PipeSpawner spawner;
    
    public RectTransform gameOverUI; // Arraste o painel do Game Over no Inspector
    public float gameOverMoveAmount = 50f; // Quanto o canvas vai subir
    public float gameOverMoveSpeed = 5f; // Velocidade da animação

    private Vector3 _gameOverStartPos;
    private Vector3 _gameOverTargetPos;
    private bool _moveGameOverUI = false;
    
    private int _score = 0;
    private bool _isPlaying = false;
    
    void Start()
    {
        getReadySprite.SetActive(true);
        if (bird != null)
            bird.GetComponent<Rigidbody2D>().simulated = false;
        if (spawner != null)
            spawner.enabled = false;
        
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(false); // começa desativado
            _gameOverStartPos = gameOverUI.localPosition; // guarda a posição inicial
            _gameOverTargetPos = _gameOverStartPos + new Vector3(0, gameOverMoveAmount, 0); // posição final
        }
    }
    
    void Update()
    {
        if (!_isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        
        if (_moveGameOverUI && gameOverUI != null)
        {
            // Move a posição atual em direção ao alvo, com velocidade constante
            gameOverUI.localPosition = Vector3.MoveTowards(
                gameOverUI.localPosition, 
                _gameOverTargetPos, 
                gameOverMoveSpeed * Time.deltaTime
            );

            // Quando chegar perto, para o movimento
            if (Vector3.Distance(gameOverUI.localPosition, _gameOverTargetPos) < 0.01f)
            {
                gameOverUI.localPosition = _gameOverTargetPos;
                _moveGameOverUI = false;
            }
        }

    }

    void StartGame()
    {
        if (getReadySprite != null)
            getReadySprite.SetActive(false);
        
        if (bird != null)
            bird.GetComponent<Rigidbody2D>().simulated = true;
        
        if (spawner != null)
            spawner.enabled = true;
    }
    
    public void IncrementScore()
    {
        _score++;
        if (scoreText != null)
        {
            scoreText.text = _score.ToString();
        }
    }

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true);   // ativa a UI
            gameOverUI.localPosition = _gameOverStartPos; // garante que começa na posição inicial
            _moveGameOverUI = true; // inicia a animação
        }
        
        var parallaxes = FindObjectsByType<Parallax>(FindObjectsSortMode.None);
        foreach (var p in parallaxes)
        {
            p.enabled = false;
        }
        
        var bird = FindAnyObjectByType<Bird>();
        
        if (bird != null)
        {
            bird.enabled = false;
        }
        
        var pipes = FindObjectsByType<MovePipe>(FindObjectsSortMode.None);
        foreach (var p in pipes)
        {
            p.enabled = false;
            
            var colliders = p.GetComponentsInChildren<Collider2D>();
            foreach (var c in colliders)
                c.enabled = false;
        }
        
        var spawner = FindAnyObjectByType<PipeSpawner>();
        if (spawner != null)
            spawner.enabled = false;
        
        Debug.Log("GameOver");
        
        GameObject.Find("Bird").transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), 0.9f);
    }
}