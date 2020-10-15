using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
#pragma warning disable 0649
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
    [SerializeField] private bool buttonPressed;
    [SerializeField] private bool isPaused;

    [Header("Countdown")]
    [SerializeField] private bool hasCounter;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject countdownScreen;
#pragma warning restore 0649

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
        isPaused = false;
        buttonPressed = false;
        
        if (hasCounter)
        {
            StartCoroutine(StartCounter());
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private IEnumerator StartCounter()
    {
        countdownScreen.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        countdownScreen.SetActive(false);
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
