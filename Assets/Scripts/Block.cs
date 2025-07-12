using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Block : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D col;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    public float Width
    {
        get => sr.size.x;
        set => SetSize(value, Height);
    }

    public float Height
    {
        get => sr.size.y;
        set => SetSize(Width, value);
    }

    public float PositionX
    {
        get => transform.position.x;
        set => SetPosition(value, PositionY);
    }

    public float PositionY
    {
        get => transform.position.y;
        set => SetPosition(PositionX, value);
    }

    public void SetSize(float width, float height)
    {
        sr.size = new Vector2(width, height);

        if (col != null)
        {
            col.size = new Vector2(width, height);
            col.offset = Vector2.zero;
        }
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0f);
    }
}
