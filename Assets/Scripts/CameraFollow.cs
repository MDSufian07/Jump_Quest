using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    [SerializeField] private Transform target; // The object for the camera to follow
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -3f); // Offset from the target

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f); // Smoothly interpolate to the desired position
        }
    }
}
