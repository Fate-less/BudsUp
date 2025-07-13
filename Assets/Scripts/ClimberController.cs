using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberController : MonoBehaviour
{
    public float climbSpeed = 2f;
    public float minClimbableWidth = 0.5f;

    private Animator animator;
    private Transform targetBlock;
    private bool isClimbing = false;
    private bool isFalling = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = StackManager.Instance.GetBaseBlockPosition();
    }

    private void Update()
    {
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
            StackManager.Instance.ShowEndScreen();
        }
    }
}
