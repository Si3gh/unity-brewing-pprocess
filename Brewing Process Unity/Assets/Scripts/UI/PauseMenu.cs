using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject pauseMenu;
    public Button pauseButton;
    public Button resumeMenuButton;
    public KeyCode pauseKey;
    public bool buttonPressed;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || buttonPressed )  {
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
        buttonPressed = false;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        buttonPressed = true;
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
        pauseButton.onClick.AddListener(PauseGame);
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
