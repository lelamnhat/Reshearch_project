using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnY = 6f;
    public float spawnInterval = 0.5f;

    public float[] lanes = { -2f, -1f, 0f, 1f, 2f };

    private float timer;

    // ===== DANH SÁCH PATTERN =====
    private int[][] patterns = new int[][]
    {
        new int[] {1,0,1,0,0},
        new int[] {0,1,0,1,0},
        new int[] {1,1,0,1,1},
        new int[] {0,0,1,0,0},
        new int[] {1,0,0,0,1},
        new int[] {1,1,1,0,0},
        new int[] {0,0,1,1,1},
        new int[] {1,0,1,0,1},
    };

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPattern();
            timer = 0f;
        }
    }

    void SpawnPattern()
    {
        int randomIndex = Random.Range(0, patterns.Length);
        int[] selectedPattern = patterns[randomIndex];

        for (int i = 0; i < selectedPattern.Length; i++)
        {
            if (selectedPattern[i] == 1)
            {
                Vector3 spawnPos = new Vector3(lanes[i], spawnY, 0);
                Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}