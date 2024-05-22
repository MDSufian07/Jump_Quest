using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SceneStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Enable the panel
        }
    }

    public void HidePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Disable the panel
        }
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Level Two");
    }

}
