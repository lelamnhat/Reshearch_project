using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    [SerializeField] private SpeedController speedController;
    [SerializeField] private TextMeshProUGUI speedText;

    void Update()
    {
        if (speedController == null || speedText == null) return;

        float speed = speedController.CurrentSpeed;

        speedText.text = "Speed x" + speed.ToString();
    }
}