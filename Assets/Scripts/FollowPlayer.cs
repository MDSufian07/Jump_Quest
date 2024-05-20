using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset;   // Offset to maintain between the player and the follower

    void Update()
    {
        if (target != null) // Ensure the target is set
        {
            // Calculate the desired position for the follower
            Vector3 desiredPosition = target.position + offset;

            // Set the position of the follower without changing its rotation
            transform.position = desiredPosition;
        }
    }
}
