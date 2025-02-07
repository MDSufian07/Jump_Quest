using UnityEngine;

public class LifeCollectible : MonoBehaviour
{
    public int lifeValue = 1; // How many lives to add

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.AddLife(lifeValue); // Add life to player
                Destroy(gameObject); // Remove collectible
            }
        }
    }
}