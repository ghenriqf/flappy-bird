using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public AudioClip clickSound;

    void PlayClickSound()
    {
        if (clickSound != null)
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
    }

    void LoadSceneWithClick(string sceneName)
    {
        PlayClickSound();
        // método da Unity usado para iniciar uma Coroutine
        StartCoroutine(DelayedLoad(sceneName, 0.5f));
    }

    IEnumerator DelayedLoad(string sceneName, float delay)
    {
        // yield palavra-chave do C# usada para pausar a execução de uma função
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    
    public void OkButton()
    {  
        LoadSceneWithClick("Level1");
    }

    public void MenuButton()
    {
        LoadSceneWithClick("MainMenu");
    }

    public void StartButton()
    {
        LoadSceneWithClick("Level1");
    }
}