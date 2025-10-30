using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OkButton()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void MenuButton() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
