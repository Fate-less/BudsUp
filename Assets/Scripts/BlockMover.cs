using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    [Header("Block settings")]
    public float moveSpeed = 2f;
    public float moveRange = 3f;
    public float direction = 1f;

    private float startX;

    private void Start()
    {
        startX = transform.position.x;
    }

    private void Update()
    {
        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(newX) > moveRange)
            direction *= -1f;

        transform.position = new Vector3(newX, transform.position.y, 0f);
    }
}