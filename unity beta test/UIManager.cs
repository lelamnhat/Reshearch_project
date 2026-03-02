using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject gameUIPanel;

    public void StartGame()
    {
        menuPanel.SetActive(false);
        gameUIPanel.SetActive(true);
    }
}