﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusic : MonoBehaviour
{
    private bool isSceneLoaded;

    private void Start()
    {
        if (!isSceneLoaded) return;

        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneLoaded = scene.name == "SampleScene" && mode == LoadSceneMode.Single;
    }
}