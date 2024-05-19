using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            try
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to load next scene: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("No next scene available to load.");
        }
    }
}
