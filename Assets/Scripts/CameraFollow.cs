using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public float verticalOffset = 2f;

    private float highestY;

    private void Start()
    {
        if (target != null)
            highestY = target.position.y + verticalOffset;
    }

    private void Update()
    {
        if (target == null) return;

        float targetY = target.position.y + verticalOffset;

        if (targetY > highestY)
            highestY = targetY;

        Vector3 newPosition = new Vector3(transform.position.x, highestY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}

