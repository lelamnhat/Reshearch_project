using System.Collections.Generic;
using UnityEngine;

public class EndlessMapSystem : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Camera cam;

    [Header("Tile Prefabs")]
    [SerializeField] private GameObject[] tilePrefabs;

    [Header("Initial Setup")]
    [SerializeField] private int startTileCount = 5;

    [Header("Spawn / Despawn")]
    [SerializeField] private float spawnAheadOffset = 8f;
    [SerializeField] private float despawnOffset = 10f;

    [Header("Snap")]
    [SerializeField] private float snapFactor = 100f;

    private readonly List<GameObject> activeTiles = new();
    private int lastPrefabIndex = -1;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    public void Init()
    {
        ClearAllTiles();

        for (int i = 0; i < startTileCount; i++)
        {
            SpawnTile();
        }

        DevLog.Info($"[EndlessMapSystem] Init complete. Active tiles: {activeTiles.Count}");
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0f) return;
        if (cam == null) return;
        if (tilePrefabs == null || tilePrefabs.Length == 0) return;

        SpawnIfNeeded();
        DestroyOldTiles();
    }

    private void SpawnIfNeeded()
    {
        if (activeTiles.Count == 0) return;

        float camTop = cam.transform.position.y + cam.orthographicSize;

        GameObject lastTile = activeTiles[^1];
        SpriteRenderer sr = lastTile.GetComponentInChildren<SpriteRenderer>();

        if (sr == null)
        {
            DevLog.Error("[EndlessMapSystem] Last tile missing SpriteRenderer.");
            return;
        }

        float lastTileTop = sr.bounds.max.y;

        if (camTop + spawnAheadOffset >= lastTileTop)
        {
            SpawnTile();
        }
    }

    private void DestroyOldTiles()
    {
        if (activeTiles.Count == 0) return;

        float camBottom = cam.transform.position.y - cam.orthographicSize - despawnOffset;

        while (activeTiles.Count > 0)
        {
            GameObject firstTile = activeTiles[0];

            if (firstTile == null)
            {
                activeTiles.RemoveAt(0);
                continue;
            }

            SpriteRenderer sr = firstTile.GetComponentInChildren<SpriteRenderer>();

            if (sr == null)
            {
                activeTiles.RemoveAt(0);
                Destroy(firstTile);
                continue;
            }

            float tileTop = sr.bounds.max.y;

            // Tile đã hoàn toàn ở dưới camera => xóa hẳn
            if (tileTop < camBottom)
            {
                activeTiles.RemoveAt(0);
                Destroy(firstTile);
                DevLog.Info($"[EndlessMapSystem] Destroy tile. Remaining: {activeTiles.Count}");
            }
            else
            {
                break;
            }
        }
    }

    private void SpawnTile()
    {
        GameObject prefab = GetRandomPrefab();
        if (prefab == null)
        {
            DevLog.Error("[EndlessMapSystem] No tile prefab available.");
            return;
        }

        Vector3 spawnPos = Vector3.zero;

        if (activeTiles.Count > 0)
        {
            GameObject lastTile = activeTiles[^1];
            SpriteRenderer lastSR = lastTile.GetComponentInChildren<SpriteRenderer>();

            if (lastSR == null)
            {
                DevLog.Error("[EndlessMapSystem] Last tile missing SpriteRenderer.");
                return;
            }

            float lastTileTop = lastSR.bounds.max.y;

            // Spawn tile mới sao cho đáy tile mới khớp với đỉnh tile cũ
            SpriteRenderer prefabSR = prefab.GetComponentInChildren<SpriteRenderer>();
            if (prefabSR == null)
            {
                DevLog.Error("[EndlessMapSystem] Prefab missing SpriteRenderer.");
                return;
            }

            float prefabHalfHeight = prefabSR.bounds.extents.y;

            spawnPos = new Vector3(
                0f,
                lastTileTop + prefabHalfHeight,
                0f
            );
        }

        spawnPos.y = Snap(spawnPos.y);

        GameObject tile = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeTiles.Add(tile);

        DevLog.Info($"[EndlessMapSystem] Spawn tile: {tile.name} | Active: {activeTiles.Count}");
    }

    private GameObject GetRandomPrefab()
    {
        if (tilePrefabs == null || tilePrefabs.Length == 0) return null;

        if (tilePrefabs.Length == 1)
        {
            lastPrefabIndex = 0;
            return tilePrefabs[0];
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        while (randomIndex == lastPrefabIndex);

        lastPrefabIndex = randomIndex;
        return tilePrefabs[randomIndex];
    }

    private void ClearAllTiles()
    {
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            if (activeTiles[i] != null)
            {
                Destroy(activeTiles[i]);
            }
        }

        activeTiles.Clear();
    }

    private float Snap(float value)
    {
        if (snapFactor <= 0f) return value;
        return Mathf.Round(value * snapFactor) / snapFactor;
    }
}