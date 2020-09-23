using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField] private bool startWithTransition;
    [SerializeField] private Animator transition;
    [SerializeField] private Slider progressSlider;
#pragma warning restore 0649

    public void Awake()
    {
        if (startWithTransition)
        {
            transition.SetTrigger("FadeIn");
        }
    }

    public void GoToMainMenu()
    {
        LoadSceneWithLoading("MainMenu");
    }
    
    public void GoToHub()
    {
        LoadSceneWithLoading("Hub");
    }
    
    public void GoToTrackScene()
    {
        LoadSceneWithLoading("TrackScene");
    }

    public void GoToDialogueScene()
    {
        LoadSceneWithLoading("Dialogue");
    }
    
    public void GoToDialogueTutorial()
    {
        LoadSceneWithLoading("DialogueTutorial");
    }
    
    public void GoToMoer_tutorial()
    {
        LoadSceneWithLoading("Moer_tutorial");
    }
    public void GoToFirstGame()
    {
        LoadSceneWithLoading("Moer");
    }

    private void LoadSceneWithLoading(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        transition.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        var operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressSlider.value = progress;
            yield return null;
        }
    }
}
    