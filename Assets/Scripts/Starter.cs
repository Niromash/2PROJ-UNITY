using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    void Start()
    {
        try
        {
            SceneManager.UnloadSceneAsync("SampleScene");
        }
        catch (System.Exception e)
        {
        }
    }
}