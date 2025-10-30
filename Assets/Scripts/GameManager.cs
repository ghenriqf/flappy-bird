using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] private MovePipe pipe;
    [SerializeField] private Text scoreText;
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
        
        Debug.Log("GameOver");
        
        GameObject.Find("Bird").transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
