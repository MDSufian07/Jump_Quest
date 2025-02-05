using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public AudioSource slideDownSound;
    public AudioSource playerDeathSound;

    public int maxLives = 5; 
    private int currentLives;
    private Vector3 lastCheckpoint; 

    public Image[] hearts; 
    public GameObject gameOverPanel; // Reference to Game Over UI
    public GameObject hduPanel;

    bool dead = false;

    private void Start()
    {
        currentLives = maxLives;
        lastCheckpoint = transform.position; 
        gameOverPanel.SetActive(false); // Hide Game Over screen at start
        UpdateHeartsUI();
    }

    private void Update()
    {
        if (transform.position.y < -5f && !dead)
        {
            slideDownSound.Play();
            TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            playerDeathSound.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), 1.3f);
        }

        dead = true;
    }

    void Respawn()
    {
        transform.position = lastCheckpoint;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<PlayerMovement>().enabled = true;
        dead = false;
    }

    void GameOver()
    {
        hduPanel.SetActive(false);
        gameOverPanel.SetActive(true); // Show Game Over UI
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives; 
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
    }
}
