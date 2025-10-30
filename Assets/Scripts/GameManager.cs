using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverUI;
    public Text gameOverScoreText;
    public Text gameOverBestText;

    public GameObject getReadySprite;
    public GameObject bird;
    public PipeSpawner spawner;

    private int _score = 0;
    private int _bestScore = 0;
    private bool _isPlaying = false;
    private bool _isGameOver = false;

    void Start()
    {
        if (_isGameOver) return;
        
        getReadySprite.SetActive(true);

        if (bird != null)
            bird.GetComponent<Rigidbody2D>().simulated = false;

        if (spawner != null)
            spawner.enabled = false;

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateGameplayUI();
    }

    void Update()
    {
        
        if (!_isPlaying && Input.GetKeyDown(KeyCode.Space) && !_isGameOver)
        {
            StartGame();
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
            gameOverUI.SetActive(false);

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
        _isGameOver = true;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

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

        GameObject.Find("Bird").transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    void UpdateGameplayUI()
    {
        if (scoreText != null)
            scoreText.text = _score.ToString();
    }

    void UpdateGameOverUI()
    {
        if (gameOverScoreText != null)
            gameOverScoreText.text = _score.ToString();

        if (gameOverBestText != null)
            gameOverBestText.text = _bestScore.ToString();
    }
}
