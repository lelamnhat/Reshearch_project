using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float maxSpeed = 30f;

    [Header("Increase Over Time")]
    [SerializeField] private float increaseAmount = 1f;      // mỗi lần tăng bao nhiêu
    [SerializeField] private float increaseInterval = 10f;   // bao nhiêu giây tăng 1 lần

    private float timer;

    public float CurrentSpeed { get; private set; }
    public float BaseSpeed => baseSpeed;

    private void Awake()
    {
        ResetSpeed();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        timer += Time.deltaTime;

        if (timer >= increaseInterval)
        {
            timer = 0f;

            CurrentSpeed += increaseAmount;
            CurrentSpeed = Mathf.Min(CurrentSpeed, maxSpeed);

            DevLog.Info($"[SpeedController] Speed = {CurrentSpeed:0.00}");
        }
    }

    public void ResetSpeed()
    {
        CurrentSpeed = baseSpeed;
        timer = 0f;
    }
}