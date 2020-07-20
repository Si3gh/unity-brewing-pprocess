using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject pauseMenu;

    public Button pauseButton;
    public Button resumeMenuButton;
    public Button resumeButton;
    public KeyCode pauseKey;

    public bool buttonPressed;

    public static bool isPaused;
    // Start is called before the first frame update
  

 
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || buttonPressed )
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

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


    public void QuitGame()
    {
        Application.Quit();
    }
    void OnEnable()
    {
        pauseButton.onClick.AddListener(PauseGame);//adds a listener for when you click the button
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
