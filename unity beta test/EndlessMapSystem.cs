using System.Collections.Generic;
using UnityEngine;

public class EndlessMapSystem : MonoBehaviour
{
    [Header("Map Setup")]
    public GameObject[] tilePrefabs;
    public int startTileCount = 6;
    public float tileHeight = 20f;

    [Header("Speed")]
    public float baseMoveSpeed = 5f;
    private float currentSpeed;

    [Header("Speed Increase")]
    public float speedIncreaseAmount = 0.5f;
    public float increaseInterval = 120f; // 2 phút

    private float timer;

    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnY;

    void Start()
    {
        Init();
        currentSpeed = baseMoveSpeed;
        spawnY = 0;

        for (int i = 0; i < startTileCount; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        MoveTiles();
        HandleSpeedIncrease();
        CheckDelete();
    }

    void SpawnTile()
    {
        if (tilePrefabs == null || tilePrefabs.Length == 0)
        {
            Debug.LogError("Chưa gán tilePrefabs!");
            return;
        }

        GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];

        Vector3 spawnPos;

        if (activeTiles.Count == 0)
        {
            spawnPos = Vector3.zero;
        }
        else
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];

            SpriteRenderer lastSR = lastTile.GetComponentInChildren<SpriteRenderer>();
            float lastHeight = lastSR.bounds.size.y;

            spawnPos = lastTile.transform.position + new Vector3(0, lastHeight, 0);
        }

        GameObject tile = Instantiate(prefab, spawnPos, Quaternion.identity);

        activeTiles.Add(tile);
    }

    void MoveTiles()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.position += Vector3.down * currentSpeed * Time.deltaTime;
        }
    }

    void CheckDelete()
    {
        if (activeTiles.Count == 0) return;

        Camera cam = Camera.main;
        float bottomLimit = cam.transform.position.y - cam.orthographicSize - 5f;

        if (activeTiles[0].transform.position.y < bottomLimit)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
            SpawnTile();
        }
    }

    void HandleSpeedIncrease()
    {
        timer += Time.deltaTime;

        if (timer >= increaseInterval)
        {
            currentSpeed += speedIncreaseAmount;
            timer = 0;
        }
    }
    public void Init()
    {
        currentSpeed = baseMoveSpeed;
        spawnY = 0;

        for (int i = 0; i < startTileCount; i++)
        {
            SpawnTile();
        }
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}