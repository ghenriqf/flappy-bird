using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
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
        var pipe = GetComponent<MovePipe>();
        if (pipe != null)
            pipe.enabled = false;
        
        Debug.Log("GameOver");
        
        GameObject.Find("Bird").transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,-90),0.9f);
    }
}
