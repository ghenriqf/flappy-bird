using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject getReadySprite;
    public GameObject bird;
    public PipeSpawner spawner;
    
    public RectTransform gameOverUI; // Arraste o painel do Game Over no Inspector
    public float gameOverMoveAmount = 50f; // Distância que o Canvas irá subir (e de onde ele vai começar)
    public float gameOverMoveSpeed = 5f; // Velocidade da animação (ajuste esse valor no Inspector!)

    private Vector3 _gameOverStartPos; // Posição INICIAL da animação (abaixo do alvo)
    private Vector3 _gameOverTargetPos; // Posição FINAL da animação (posição de repouso na tela)
    private bool _moveGameOverUI = false;
    
    private int _score = 0;
    private bool _isPlaying = false;
    
    void Start()
    {
        _isPlaying = false;
        _score = 0;
        
        getReadySprite.SetActive(true);
        
        if (bird != null)
            bird.GetComponent<Rigidbody2D>().simulated = false;
        if (spawner != null)
            spawner.enabled = false;
        
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(false); // começa desativado
            
            // 1. Guarda a posição FINAL (onde a UI deve parar, a posição do editor)
            _gameOverTargetPos = gameOverUI.localPosition; 
            
            // 2. Define a posição INICIAL: a posição final menos o deslocamento para baixo
            // Se o MoveAmount for 50, ele começará 50 unidades abaixo do alvo.
            _gameOverStartPos = _gameOverTargetPos - new Vector3(0, gameOverMoveAmount, 0); 
        }
    }
    
    void Update()
    {
        if (!_isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        
        // Lógica de animação da UI de Game Over
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
        _isPlaying = true;
        
        if (getReadySprite != null)
            getReadySprite.SetActive(false);
        
        if (bird != null)
        {
            bird.GetComponent<Rigidbody2D>().simulated = true;
            // Opcional: Chama o método Flap no Bird se você tiver um script Bird
            // bird.GetComponent<Bird>().Flap(); 
        }
        
        if (spawner != null)
            spawner.enabled = true;
    }
    
    public void IncrementScore()
    {
        if (_isPlaying)
        {
            _score++;
            if (scoreText != null)
            {
                scoreText.text = _score.ToString();
            }
        }
    }

    public void GameOver()
    {
        _isPlaying = false;
        
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true);   // ativa a UI
            // Garante que a UI começa na posição INICIAL (abaixo do alvo)
            gameOverUI.localPosition = _gameOverStartPos; 
            _moveGameOverUI = true; // inicia a animação de subir!
        }
        
        // CORRIGIDO: Removido FindObjectsSortMode.None
        var parallaxes = FindObjectsOfType<Parallax>();
        foreach (var p in parallaxes)
        {
            p.enabled = false;
        }
        
        var birdComponent = FindAnyObjectByType<Bird>();
        
        if (birdComponent != null)
        {
            birdComponent.enabled = false;
        }
        
        // Desativa a movimentação e colisores dos pipes existentes
        // CORRIGIDO: Removido FindObjectsSortMode.None
        var pipes = FindObjectsOfType<MovePipe>();
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
        
        // Anima a rotação do Bird para cair
        GameObject.Find("Bird").transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), 0.9f);
    }
}