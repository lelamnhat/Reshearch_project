using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private UIManager ui;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private EndlessMapSystem mapSystem;
    [SerializeField] private ScoreManager scoreManager;

    private enum GameState { StartMenu, Playing, Paused, Credits }
    private GameState state = GameState.StartMenu;

    private void Start()
    {
        GoToMenu();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (state == GameState.Playing) PauseGame();
            else if (state == GameState.Paused) ResumeGame();
            else if (state == GameState.Credits) GoToMenu();
        }
    }

    public void StartGame()
    {
        ui?.ShowPlaying();

        speedController?.ResetSpeed();
        scoreManager?.ResetScore();
        mapSystem?.Init();

        Time.timeScale = 1f;
        state = GameState.Playing;

        DevLog.Info("[GameManager] StartGame");
    }

    public void PauseGame()
    {
        if (state != GameState.Playing) return;

        ui?.ShowPause();
        Time.timeScale = 0f;
        state = GameState.Paused;

        DevLog.Info("[GameManager] Pause");
    }

    public void ResumeGame()
    {
        if (state != GameState.Paused) return;

        ui?.ShowPlaying();
        Time.timeScale = 1f;
        state = GameState.Playing;

        DevLog.Info("[GameManager] Resume");
    }

    public void OpenCredits()
    {
        if (state == GameState.Playing) return;

        ui?.ShowCredits();
        Time.timeScale = 0f;
        state = GameState.Credits;
    }

    public void GoToMenu()
    {
        ui?.ShowStart();
        Time.timeScale = 0f;
        state = GameState.StartMenu;

        scoreManager?.ResetScore();
        DevLog.Info("[GameManager] Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}