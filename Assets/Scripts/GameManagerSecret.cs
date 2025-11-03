using UnityEngine;
using UnityEngine.UI;

public class GameManagerSecret : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverUI;
    public Text gameOverScoreText;
    public Text gameOverBestText;

    public GameObject bird;
    public PipeSpawner spawner;

    private int _score = 0;
    private int _bestScore = 0;
    private bool _isGameOver = false;

    void Start()
    {
        gameOverUI.SetActive(false);

        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
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
        if (_isGameOver) return;
        _isGameOver = true;
        
        gameOverUI.SetActive(true);
        
        var parallaxes = FindObjectsByType<Parallax>(FindObjectsSortMode.None);
        foreach (var p in parallaxes)
            p.enabled = false;

        var pipes = FindObjectsByType<MovePipeSecret>(FindObjectsSortMode.None);
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
        
        var bird = FindAnyObjectByType<Bird>();
        bird.enabled = false;
        bird.transform.rotation = Quaternion.Euler(0, 0, -90);
        
        UpdateGameOverUI();
    }

    void UpdateGameplayUI()
    {
        if (scoreText != null)
            scoreText.text = _score.ToString();
    }

    void UpdateGameOverUI()
    {
        gameOverScoreText.text = _score.ToString();
        gameOverBestText.text = _bestScore.ToString();
    }
}