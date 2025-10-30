using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI - Gameplay")]
    public Text scoreText;              
    
    [Header("UI - Game Over")]
    public Text gameOverScoreText;      
    public Text gameOverBestText;       
    public RectTransform gameOverUI;

    [Header("Game Elements")]
    public GameObject getReadySprite;
    public GameObject bird;
    public PipeSpawner spawner;

    [Header("Game Over Animation")]
    public float gameOverMoveAmount = 50f;
    public float gameOverMoveSpeed = 5f;

    private Vector3 _gameOverStartPos;
    private Vector3 _gameOverTargetPos;
    private bool _moveGameOverUI = false;

    private int _score = 0;
    private int _bestScore = 0;
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
            gameOverUI.gameObject.SetActive(false);
            _gameOverStartPos = gameOverUI.localPosition;
            _gameOverTargetPos = _gameOverStartPos + new Vector3(0, gameOverMoveAmount, 0);
        }
        
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);

        UpdateGameplayUI();
    }

    void Update()
    {
        if (!_isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (_moveGameOverUI && gameOverUI != null)
        {
            gameOverUI.localPosition = Vector3.MoveTowards(
                gameOverUI.localPosition,
                _gameOverTargetPos,
                gameOverMoveSpeed * Time.deltaTime
            );

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
        _score = 0;

        if (getReadySprite != null)
            getReadySprite.SetActive(false);

        if (bird != null)
            bird.GetComponent<Rigidbody2D>().simulated = true;

        if (spawner != null)
            spawner.enabled = true;

        if (gameOverUI != null)
            gameOverUI.gameObject.SetActive(false);

        UpdateGameplayUI();
    }

    public void IncrementScore()
    {
        _score++;
        
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("BestScore", _bestScore);
            PlayerPrefs.Save();
        }

        UpdateGameplayUI();
    }

    public void GameOver()
    {
        _isPlaying = false;

        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.localPosition = _gameOverStartPos;
            _moveGameOverUI = true;
        }
        
        UpdateGameOverUI();
        
        var parallaxes = FindObjectsByType<Parallax>(FindObjectsSortMode.None);
        foreach (var p in parallaxes)
            p.enabled = false;

        var bird = FindAnyObjectByType<Bird>();
        if (bird != null)
            bird.enabled = false;

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
        
        GameObject.Find("Bird").transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, -90),
            0.9f
        );
    }
    
    private void UpdateGameplayUI()
    {
        if (scoreText != null)
            scoreText.text = _score.ToString();
    }
    
    private void UpdateGameOverUI()
    {
        if (gameOverScoreText != null)
            gameOverScoreText.text = _score.ToString();

        if (gameOverBestText != null)
            gameOverBestText.text = _bestScore.ToString();
    }
}