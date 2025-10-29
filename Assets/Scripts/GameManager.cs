using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene("Space");
    }
}
