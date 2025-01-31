using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public AudioSource slideDownSound;
    public AudioSource playerDethSound;

    bool dead = false;
    private void Update()
    {
        if (transform.position.y < -5f && !dead)
        {
            slideDownSound.Play();
            Die();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            playerDethSound.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            Die();
        }
    }
    void Die()
    {

        Invoke(nameof(ReloadLevel), 1.3f);
        dead = true;

    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

