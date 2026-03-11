using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private SpeedController speedController;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Score Settings")]
    [SerializeField] private float scorePerSecondAtBaseSpeed = 10f;

    private float score;
    private int lastShown = -1;

    private void Awake()
    {
        Show(0);
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        if (speedController == null || scoreText == null) return;

        float baseSpeed = Mathf.Max(0.01f, speedController.BaseSpeed);
        float factor = speedController.CurrentSpeed / baseSpeed;

        score += scorePerSecondAtBaseSpeed * factor * Time.deltaTime;

        int s = Mathf.FloorToInt(score);
        Show(s);
    }

    public void ResetScore()
    {
        score = 0f;
        Show(0);
    }

    private void Show(int value)
    {
        if (value == lastShown) return;
        lastShown = value;
        scoreText.text = "Distance " + value.ToString() + "m";
    }
}