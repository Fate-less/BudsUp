using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum SpawnDirection { Left, Right }

public class StackManager : MonoBehaviour
{
    [Header("References")]
    public GameObject baseBlockPrefab;
    public GameObject blockPrefab;
    public Sprite[] blockPrefabSkins;
    public Transform stackRoot;
    public GameObject comboTextPrefab;
    public TextMeshPro scoreTMP;
    public GameObject endGameUI;
    public CharacterPassive activeCharacter;
    [Header("Stack Settings")]
    public float initialWidth = 2f;
    public float initialHeight = 2f;
    public float blockHeight = 1f;
    public float perfectThreshold = 0.05f;
    public int coin = 0;
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
    private float standardWidth = 1.5f;
    private int scoreMultiplier = 1;
    private float blockSpeedMultiplier = 1f;
    private int forcedPerfectStacksRemaining = 0;

    private void Start()
    {
        GameObject baseBlockObj = Instantiate(baseBlockPrefab, stackRoot.position, Quaternion.identity, stackRoot);
        Block baseBlock = baseBlockObj.GetComponent<Block>();
        baseBlock.SetSize(initialWidth, initialHeight);

        previousBlock = baseBlock;

        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
            cameraFollow.target = baseBlock.transform;

        activeCharacter?.OnGameStart(this);

        SpawnNextBlock();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlaceBlock();
    }

    private void SpawnNextBlock()
    {
        if (previousBlock == null)
        {
            Debug.LogError("Previous block is null! Make sure it's assigned before spawning the next block.");
            return;
        }

        SpawnDirection direction = (Random.value < 0.5f) ? SpawnDirection.Left : SpawnDirection.Right;
        float spawnX = (direction == SpawnDirection.Left) ? -3f : 3f;
        float overlapOffset = (blockCount == 0) ? 0.1f : 0f;
        float spawnY = previousBlock.PositionY + (previousBlock.Height / 2f) + (blockHeight / 2f) - overlapOffset;

        GameObject newBlockObj = Instantiate(blockPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity, stackRoot);
        newBlockObj.GetComponent<SpriteRenderer>().sprite = blockPrefabSkins[Random.Range(0, blockPrefabSkins.Length)];
        Block newBlock = newBlockObj.GetComponent<Block>();

        float newBlockWidth;

        if (blockCount == 0)
        {
            newBlockWidth = standardWidth;
        }
        else
        {
            newBlockWidth = previousBlock.Width;
        }

        newBlock.SetSize(newBlockWidth, blockHeight);

        BlockMover mover = newBlockObj.AddComponent<BlockMover>();
        mover.moveSpeed = moveSpeed * blockSpeedMultiplier;
        mover.moveRange = moveRange;
        mover.direction = (direction == SpawnDirection.Left) ? 1f : -1f;

        if (cameraFollow != null)
            cameraFollow.target = newBlock.transform;

        blockCount++;
    }

    private void PlaceBlock()
    {
        BlockMover mover = FindObjectOfType<BlockMover>();
        if (mover == null) return;

        Block currentBlock = mover.GetComponent<Block>();
        Destroy(mover);

        float offset = currentBlock.PositionX - previousBlock.PositionX;

        float previousWidth = (blockCount == 1) ? standardWidth : previousBlock.Width;

        float overlap = previousWidth - Mathf.Abs(offset);

        if (overlap <= 0f)
        {
            Debug.Log("Game Over!");
            endGameUI.SetActive(true);
            return;
        }

        float newWidth = overlap;
        float newX = previousBlock.PositionX + (offset / 2f);

        if (forcedPerfectStacksRemaining > 0 || Mathf.Abs(offset) <= perfectThreshold)
        {
            comboCount++;
            score += comboCount;
            if (forcedPerfectStacksRemaining > 0)
                forcedPerfectStacksRemaining--;
            Debug.Log($"Perfect Stack! Combo {comboCount}");

            newWidth = previousWidth;
            newX = previousBlock.PositionX;

            activeCharacter?.OnPerfectStack(this);
            ShowPerfectEffect(currentBlock.transform.position);
            ShowComboText(currentBlock.transform.position + Vector3.up * 1.5f, comboCount);
        }
        else
        {
            comboCount = 0;
            activeCharacter?.OnComboBreak(this);
            score++;
        }
        ShowScoreText(scoreTMP, score);

        currentBlock.SetSize(newWidth, blockHeight);
        currentBlock.PositionX = newX;

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
        tmp.text = $"{score}";
    }

    public void AddCoins(int amount)
    {
        coin += amount;
    }

    public void ApplyScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;
    }

    public void SetBlockSpeedMultiplier(float multiplier)
    {
        blockSpeedMultiplier = multiplier;
    }

    public void ResetBlockSpeedMultiplier()
    {
        blockSpeedMultiplier = 1f;
    }

    public void ForcePerfectStacks(int count)
    {
        forcedPerfectStacksRemaining = count;
    }
}