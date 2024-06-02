using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    private bool isPaused;
    private float previousTimeScale;

    public static float requestedTimeScale = -1f;

    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        isPaused = false;
        previousTimeScale = 1f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene" && Input.GetKeyDown(KeyCode.Escape) &&
            GameManager.GetGameState().Equals(GameState.Playing))
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
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isPaused = true;
        pauseCanvas.gameObject.SetActive(true);
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = requestedTimeScale > 0 ? requestedTimeScale : previousTimeScale;
        requestedTimeScale = -1f;
        isPaused = false;
        pauseCanvas.gameObject.SetActive(false);
        Debug.Log("Game Resumed");
    }

    public void QuitGame()
    {
        GameManager.SetGameState(GameState.NotStarted);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public static bool IsGamePaused()
    {
        return Time.timeScale == 0;
    }
}