using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float width => transform.localScale.x;
    public float height => transform.localScale.y;
    public float positionX => transform.position.x;

    public void SetSize(float newWidth)
    {
        transform.localScale = new Vector3(newWidth, height, 1f);
    }

    public void SetPositionX(float x)
    {
        transform.position = new Vector3(x, transform.position.y, 0f);
    }

    public void MoveY(float yOffset)
    {
        transform.position += new Vector3(0f, yOffset, 0f);
    }
}
