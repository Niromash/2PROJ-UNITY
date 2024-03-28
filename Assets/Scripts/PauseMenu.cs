using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void Update()
    {
        // Todo: Add code to pause the game when the escape key is pressed only on the game scene
        if (SceneManager.GetActiveScene().name == "SampleScene" && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("space key was pressed");
        }
 
    }
}
