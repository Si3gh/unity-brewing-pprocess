using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
    }
    
    public void GoToMainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToTrackScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TrackScene");
    }
    
    public void GoToFirstGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Moer");
    }
    
    
}
    