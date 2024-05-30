using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public AudioSource ClickSound;

    public float soundDelay = 0.2f; // Adjust this to the length of your click sound

    private void PlaySound()
    {
        if (ClickSound != null && ClickSound.gameObject.activeInHierarchy && ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }
    public void StartMenu()
    {
        ClickSound.Play();
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        ClickSound.Play();
        Application.Quit();
    }
    public void SceneStart()
    {
        ClickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowPanel()
    {
        if (panel != null)
        {
            PlaySound();
            panel.SetActive(true);
        }
    }

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
        yield return new WaitForSeconds(ClickSound.clip.length);
        panel.SetActive(false);
    }

    public void LevelOne()
    {
        ClickSound.Play();
        SceneManager.LoadScene("LevelOne");
    }
    public void LevelTwo()
    {
        ClickSound.Play();
        SceneManager.LoadScene("Level Two");
    }

}
