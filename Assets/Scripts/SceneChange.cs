using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private GameObject panel; // Pause Menu Panel
    [SerializeField] private GameObject hduPanel; // HUD Panel
    public AudioSource clickSound; // Click sound effect

    public float soundDelay = 0.2f; // Delay to let sound play before changing scenes
    private bool isPaused = false; // Track pause state

    private void PlaySound()
    {
        if (clickSound != null && clickSound.gameObject.activeInHierarchy && clickSound.enabled)
        {
            clickSound.Play();
        }
    }

    // Start Menu Scene
    public void StartMenu()
    {
        PlaySound();
        StartCoroutine(LoadSceneAfterSound("StartMenu"));
    }

    // Quit the Game
    public void QuitGame()
    {
        PlaySound();
        Application.Quit();
    }

    // Load Next Scene (for Start Button)
    public void SceneStart()
    {
        StartCoroutine(LoadSceneAfterSound(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Load Specific Level
    public void LevelOne()
    {
        StartCoroutine(LoadSceneAfterSound("LevelOne"));
    }

    public void LevelTwo()
    {
        StartCoroutine(LoadSceneAfterSound("Level Two"));
    }

    // Pause the game
    public void ShowPanel()
    {
        if (panel != null)
        {
            PlaySound();
            panel.SetActive(true);
            hduPanel.SetActive(false);
            Time.timeScale = 0f; // Pause the game
            isPaused = true;
        }
    }

    // Resume the game
    public void HidePanel()
    {
        if (panel != null)
        {
            PlaySound();
            StartCoroutine(HidePanelAfterSound());
           
        }
    }

    private IEnumerator HidePanelAfterSound()
    {
        yield return new WaitForSecondsRealtime(clickSound.clip.length);
        panel.SetActive(false);
        hduPanel.SetActive(true);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    // Load Scene After Sound Delay
    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        PlaySound();
        yield return new WaitForSecondsRealtime(clickSound.clip.length);
        Time.timeScale = 1f; // Ensure normal speed
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadSceneAfterSound(int sceneIndex)
    {
        PlaySound();
        yield return new WaitForSecondsRealtime(clickSound.clip.length);
        Time.timeScale = 1f; // Ensure normal speed
        SceneManager.LoadScene(sceneIndex);
    }
}
