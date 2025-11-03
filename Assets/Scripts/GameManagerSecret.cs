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
    private bool _isPlaying = false;
    private bool _isGameOver = false;

    void Start()
    {
        if (_isGameOver) return;

        // ✅ Cena secreta começa direto (sem get ready)
        _isPlaying = true;

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // Ativa o pássaro e o spawner imediatamente
        if (bird != null)
        {
            var rb = bird.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.simulated = true;
        }

        if (spawner != null)
            spawner.enabled = true;

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
        _isPlaying = false;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        UpdateGameOverUI();

        // 🔻 Mesmo comportamento da cena normal 🔻
        var parallaxes = FindObjectsByType<Parallax>(FindObjectsSortMode.None);
        foreach (var p in parallaxes)
            p.enabled = false;

        var bird = FindAnyObjectByType<Bird>();
        if (bird != null)
            bird.enabled = false;

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

        // faz o pássaro cair
        GameObject birdObj = GameObject.Find("Bird");
        if (birdObj != null)
            birdObj.transform.rotation = Quaternion.Euler(0, 0, -90);
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