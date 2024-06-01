using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void LoadSinglePlayer()
    { 
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
}
