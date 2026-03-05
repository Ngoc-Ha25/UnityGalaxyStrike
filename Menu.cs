using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("VuTru"); // thay bằng tên scene của bạn
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu"); // thay bằng tên scene của bạn
    }

}