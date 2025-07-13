using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberController : MonoBehaviour
{
    public float climbSpeed = 2f;
    public float minClimbableWidth = 0.5f;
    public float horizontalSpeed = 5f;

    [Header("Horizontal Clamp")]
    public float minX = -3f;
    public float maxX = 3f;

    private Animator animator;
    private Transform targetBlock;
    private Vector2 touchStartPos;
    private float swipeThreshold = 50f;
    private bool isClimbing = false;
    private bool isFalling = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (isFalling)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += move * horizontalSpeed * Time.deltaTime;
        }
    }

    private void HandleSwipeInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            touchStartPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchEndPos = Input.mousePosition;
            Vector2 delta = touchEndPos - touchStartPos;

            if (Mathf.Abs(delta.x) > swipeThreshold)
            {
                float direction = Mathf.Sign(delta.x);
                MoveClimberHorizontally(direction);
            }
        }
#else
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                break;
            case TouchPhase.Ended:
                Vector2 delta = touch.position - touchStartPos;
                if (Mathf.Abs(delta.x) > swipeThreshold)
                {
                    float direction = Mathf.Sign(delta.x);
                    MoveClimberHorizontally(direction);
                }
                break;
        }
    }
#endif
    }

    private void MoveClimberHorizontally(float direction)
    {
        if (isFalling)
        {
            float moveAmount = direction * horizontalSpeed * Time.deltaTime * 30f;
            float newX = Mathf.Clamp(transform.position.x + moveAmount, minX, maxX);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        HandleSwipeInput();

        if (isClimbing && targetBlock != null)
        {
            Vector3 targetPos = new Vector3(targetBlock.position.x, targetBlock.position.y + 0.5f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, climbSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
                isClimbing = false;
        }

        if (isFalling)
        {
            transform.position += Vector3.down * (climbSpeed / 2f) * Time.deltaTime;
        }
    }

    public void ClimbToBlock(Transform block, float width)
    {
        if (width < minClimbableWidth)
        {
            JumpOff(block);
            return;
        }

        targetBlock = block;
        isClimbing = true;
        animator.Play("Climb");
    }

    private void JumpOff(Transform lastBlock)
    {
        isClimbing = false;
        StackManager.Instance.SetGameOver(true);
        Camera.main.GetComponent<CameraFollow>().SetTarget(transform, true);
        transform.position = new Vector3(lastBlock.position.x, lastBlock.position.y + 1f, transform.position.z);
        animator.Play("JumpOff");
        Invoke(nameof(ActivateParachute), 0.5f);
    }

    private void ActivateParachute()
    {
        isFalling = true;
        animator.Play("Parachute");
        CoinManager.Instance.SpawnCoinsAlongStack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            StackManager.Instance.AddCoins(1);
        }

        if (other.CompareTag("Ground") && isFalling)
        {
            isFalling = false;
            animator.Play("Land");
            Camera.main.GetComponent<CameraFollow>().SetStopFollow(true);
            StackManager.Instance.ShowEndScreen();
        }
    }


}
