using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    public Button normalSpeedButton;
    public Button doubleSpeedButton;
    public Button tripleSpeedButton;

    private void Start()
    {
        normalSpeedButton.onClick.AddListener(() => RequestGameSpeed(1f));
        doubleSpeedButton.onClick.AddListener(() => RequestGameSpeed(2f));
        tripleSpeedButton.onClick.AddListener(() => RequestGameSpeed(3f));
    }

    private void RequestGameSpeed(float speed)
    {
        if (PauseMenu.IsGamePaused())
        {
            PauseMenu.requestedTimeScale = speed;
        }
        else
        {
            SetGameSpeed(speed);
        }
    }

    private void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}