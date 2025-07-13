using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float verticalOffset = 2f;

    [Header("Clamp Settings")]
    public float minYLimit = 0f;  // <- lowest camera Y position allowed

    private float highestY;
    private bool stopFollowing = false;
    private bool allowFollowDownward = false;

    private void Start()
    {
        if (target != null)
            highestY = target.position.y + verticalOffset;
    }

    private void Update()
    {
        if (target == null || stopFollowing) return;

        float targetY = target.position.y + verticalOffset;

        // Update highestY according to follow mode
        if (allowFollowDownward)
            highestY = targetY;
        else if (targetY > highestY)
            highestY = targetY;

        // Clamp the target Y to prevent camera from dipping too low
        float clampedY = Mathf.Max(highestY, minYLimit);

        Vector3 newPosition = new Vector3(transform.position.x, clampedY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }

    public void SetStopFollow(bool value)
    {
        stopFollowing = value;
    }

    public void SetTarget(Transform newTarget, bool allowDown = false)
    {
        target = newTarget;
        allowFollowDownward = allowDown;

        if (target != null)
        {
            highestY = target.position.y + verticalOffset;
            stopFollowing = false;
        }
    }
}