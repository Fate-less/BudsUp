using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float duration = 1f;

    private TextMeshPro textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        textMesh.alpha -= Time.deltaTime / duration;
    }
}