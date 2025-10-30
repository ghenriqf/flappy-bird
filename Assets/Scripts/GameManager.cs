using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject getReadySprite;
    public GameObject bird;
    public PipeSpawner spawner;
    
    private int _score = 0;
    private bool _isPlaying = false;
    
    void Start()
    {
        getReadySprite.SetActive(true);
        if (bird != null)
            bird.GetComponent<Rigidbody2D>().simulated = false;
        if (spawner != null)
            spawner.enabled = false;
    }
    
    void Update()
    {
        if (!_isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
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
