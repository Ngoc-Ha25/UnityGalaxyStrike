using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinManager : MonoBehaviour
{
    public GameObject gameWinUI;

    void Start()
    {
        if (gameWinUI != null)
        {
            gameWinUI.SetActive(false);
        }
    }

    public void ShowGameWin()
    {
        gameWinUI.SetActive(true);
        Time.timeScale = 1f; // Dừng game khi thắng
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset lại tốc độ game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}