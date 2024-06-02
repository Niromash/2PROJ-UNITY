using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.SetGameState(GameState.NotStarted);
        SceneManager.LoadScene("MainMenu");
    }
}
