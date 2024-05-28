using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    private bool isPaused;

    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene" && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseCanvas.gameObject.SetActive(true);
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseCanvas.gameObject.SetActive(false);
        Debug.Log("Game Resumed");
    }

    public void QuitGame()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("MainMenu");
    }
}