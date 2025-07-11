using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectStackEffect : MonoBehaviour
{
    public Color flashColor = Color.yellow;
    public float duration = 0.15f;

    private SpriteRenderer sr;
    private Color originalColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        originalColor = sr.color;
        StartCoroutine(Flash());
    }

    private System.Collections.IEnumerator Flash()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;
        Destroy(this);
    }
}

