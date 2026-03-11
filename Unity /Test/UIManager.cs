using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject creditsPanel;

    public void ShowStart()
    {
        if (startPanel) startPanel.SetActive(true);
        if (pausePanel) pausePanel.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(false);
    }

    public void ShowPlaying()
    {
        if (startPanel) startPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(false);
    }

    public void ShowPause()
    {
        if (startPanel) startPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(true);
        if (creditsPanel) creditsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        if (startPanel) startPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(true);
    }
}