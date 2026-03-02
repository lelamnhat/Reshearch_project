using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public EndlessMapSystem mapSystem;

    private float score;
    private float scoreMultiplier = 1f;

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (mapSystem == null) return;

        score += scoreMultiplier * Time.deltaTime;

        scoreText.text = "" + Mathf.FloorToInt(score);
    }

    public void IncreaseMultiplier(float amount)
    {
        scoreMultiplier += amount;
    }

    public void ResetScore()
    {
        score = 0f;
        scoreMultiplier = 1f;
        scoreText.text = "0";
    }
}