using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakButtons : MonoBehaviour
{
    public void Return(string sceneName)
    {
        SpeedController.Instance.ChangeEnabled();
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void Restart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Menu(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
