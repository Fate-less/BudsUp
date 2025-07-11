using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum SpawnDirection { Left, Right }

public class StackManager : MonoBehaviour
{
    [Header("References")]
    public GameObject blockPrefab;
    public Transform stackRoot;
    public GameObject comboTextPrefab;
    public TextMeshPro scoreTMP;
    [Header("Stack Settings")]
    public float blockHeight = 1f;
    public float perfectThreshold = 0.05f;
    [Header("Combo Settings")]
    public int comboCount = 0;
    public int maxComboToDisplay = 10;
    [Header("Block Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;

    public int score = 0;

    private Block previousBlock;
    private int blockCount = 0;
    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        SpawnInitialBlock();
        SpawnNextBlock();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlaceBlock();
    }

    private void SpawnInitialBlock()
    {
        GameObject baseBlock = Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, stackRoot);
        previousBlock = baseBlock.GetComponent<Block>();
        previousBlock.SetSize(3f);
        if (cameraFollow != null)
            cameraFollow.target = baseBlock.transform;
    }

    private void SpawnNextBlock()
    {
        SpawnDirection direction = (Random.value < 0.5f) ? SpawnDirection.Left : SpawnDirection.Right;

        float spawnX = (direction == SpawnDirection.Left) ? -3f : 3f;
        float spawnY = previousBlock.transform.position.y + blockHeight;

        GameObject newBlock = Instantiate(blockPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity, stackRoot);
        newBlock.transform.localScale = new Vector3(previousBlock.width, blockHeight, 1f);

        BlockMover mover = newBlock.AddComponent<BlockMover>();
        mover.moveSpeed = moveSpeed;
        mover.moveRange = moveRange;
        mover.direction = (direction == SpawnDirection.Left) ? 1f : -1f;

        SpriteRenderer sr = newBlock.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.flipX = (direction == SpawnDirection.Right);

        if (cameraFollow != null)
            cameraFollow.target = newBlock.transform;
    }

    private void PlaceBlock()
    {
        BlockMover mover = FindObjectOfType<BlockMover>();
        if (mover == null) return;

        Block currentBlock = mover.GetComponent<Block>();
        Destroy(mover);

        float offset = currentBlock.positionX - previousBlock.positionX;
        float overlap = previousBlock.width - Mathf.Abs(offset);

        if (overlap <= 0f)
        {
            Debug.Log("Game Over!");
            return;
        }

        float newWidth = overlap;
        float newX = previousBlock.positionX + offset / 2f;

        if (Mathf.Abs(offset) <= perfectThreshold)
        {
            comboCount++;
            score += 10 + (comboCount * 5);
            Debug.Log($"Perfect Stack! Combo: {comboCount}");

            ShowPerfectEffect(currentBlock.transform.position);
            ShowComboText(currentBlock.transform.position + Vector3.up * 1.5f, comboCount);
            ShowScoreText(scoreTMP, score);

            newWidth = previousBlock.width;
            newX = previousBlock.positionX;
        }
        else
        {
            comboCount = 0;
        }

        currentBlock.SetSize(newWidth);
        currentBlock.SetPositionX(newX);

        previousBlock = currentBlock;
        SpawnNextBlock();
    }

    private void ShowPerfectEffect(Vector3 position)
    {
        Block currentBlock = FindObjectOfType<Block>();
        currentBlock.gameObject.AddComponent<PerfectStackEffect>();

        // Keluar suara sm reaction budsnya disini
    }

    private void ShowComboText(Vector3 position, int combo)
    {
        if (combo < 2) return;

        GameObject popup = Instantiate(comboTextPrefab, position, Quaternion.identity);
        TextMeshPro tmp = popup.GetComponent<TextMeshPro>();
        tmp.text = $"Combo x{combo}!";
    }

    private void ShowScoreText(TextMeshPro tmp, int score)
    {
        if (score < 0) return;
        tmp.text = $"Score: {score}";
    }
}