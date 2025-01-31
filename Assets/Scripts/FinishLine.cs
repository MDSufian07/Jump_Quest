using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public AudioSource finishLevelSound;
    public GameObject fireworksPrefab;  // Reference to the fireworks prefab
    private bool hasFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            hasFinished = true;
            finishLevelSound.Play();
            InstantiateFireworks(other.transform.position);  // Instantiate fireworks at the player's position
            StartCoroutine(LoadNextLevelAfterSound());
        }
    }

    private void InstantiateFireworks(Vector3 position)
    {
        // Instantiate the fireworks at the player's position
        Instantiate(fireworksPrefab, position, Quaternion.identity);
    }

    private IEnumerator LoadNextLevelAfterSound()
    {
        // Wait until the sound has finished playing
        yield return new WaitForSeconds(finishLevelSound.clip.length);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
    