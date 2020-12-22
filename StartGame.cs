using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        SaveSystem.Initialize("results");
    }
    public void ScreenSwitching()
    {
        SceneManager.LoadScene("space game");
    }
}
