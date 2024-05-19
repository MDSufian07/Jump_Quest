using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 lastPlatformPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            lastPlatformPosition = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition;
            playerTransform.position += platformMovement;
            lastPlatformPosition = transform.position;
        }
    }
}
