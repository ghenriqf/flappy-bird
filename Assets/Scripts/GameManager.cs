using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverUI;
    public Text gameOverScoreText;
    public Text gameOverBestText;

    public GameObject getReadySprite;
    public GameObject portal;
    public GameObject bird;
    public PipeSpawner spawner;

    private int _score = 0;
    private int _bestScore = 0;
    private bool _isPlaying = false;
    private bool _isGameOver = false;

    void Start()
    {
        portal.SetActive(false);
        getReadySprite.SetActive(true);
        gameOverUI.SetActive(false);
        
        bird.GetComponent<Rigidbody2D>().simulated = false;
        spawner.enabled = false;

        // PlayerPrefs é uma forma de armazenar dados simples de forma persistente no Unity
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        
        UpdateGameplayUI();
    }

    void Update()
    {
        if (!_isPlaying && Input.GetKeyDown(KeyCode.Space) && !_isGameOver)
            StartGame();

        if (_score == 30 && !portal.activeSelf)
            portal.SetActive(true);
    }

    void StartGame()
    {
        _isPlaying = true;
        _score = 0;
        
        getReadySprite.SetActive(false);
        gameOverUI.SetActive(false);
        
        bird.GetComponent<Rigidbody2D>().simulated = true;
        
        spawner.enabled = true;
        
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
        
        gameOverUI.SetActive(true);

        UpdateGameOverUI();
        DisableAllGameObjects();

        GameObject.Find("Bird").transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    void DisableAllGameObjects()
    {
        foreach (var p in FindObjectsByType<Parallax>(FindObjectsSortMode.None))
            p.enabled = false;

        foreach (var pipe in FindObjectsByType<MovePipe>(FindObjectsSortMode.None))
        {
            pipe.enabled = false;
            foreach (var c in pipe.GetComponentsInChildren<Collider2D>())
            {
                c.enabled = false;
            }
        }

        var spawner = FindAnyObjectByType<PipeSpawner>();
        spawner.enabled = false;

        var bird = FindAnyObjectByType<Bird>();
        bird.enabled = false;
    }

    void UpdateGameplayUI()
    {
        scoreText.text = _score.ToString();
    }

    void UpdateGameOverUI()
    {
        gameOverScoreText.text = _score.ToString();
        gameOverBestText.text = _bestScore.ToString();
    }
}