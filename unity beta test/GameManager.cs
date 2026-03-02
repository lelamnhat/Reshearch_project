using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject creditsPanel;
    public GameObject spawner;

    public ScoreManager scoreManager;

    private enum GameState
    {
        StartMenu,
        Playing,
        Paused,
        Credits
    }

    private GameState currentState;

    void Start()
    {
        currentState = GameState.StartMenu;

        if (startPanel) startPanel.SetActive(true);
        if (pausePanel) pausePanel.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(false);
        if (spawner) spawner.SetActive(false);

        if (scoreManager) scoreManager.ResetScore();

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                ResumeGame();
            else if (currentState == GameState.Credits)
                BackToStart();
        }
    }

    public void StartGame()
    {
        if (startPanel) startPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(false);

        if (spawner)
        {
            spawner.SetActive(true);

            EndlessMapSystem map = spawner.GetComponent<EndlessMapSystem>();
            if (map != null)
                map.Init();
        }

        if (scoreManager)
            scoreManager.ResetScore();

        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        if (pausePanel) pausePanel.SetActive(true);
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void ResumeGame()
    {
        if (pausePanel) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void OpenCredits()
    {
        if (startPanel) startPanel.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(true);

        currentState = GameState.Credits;
    }

    public void BackToStart()
    {
        if (creditsPanel) creditsPanel.SetActive(false);
        if (startPanel) startPanel.SetActive(true);

        currentState = GameState.StartMenu;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}