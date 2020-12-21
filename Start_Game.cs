using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Game : MonoBehaviour
{
    void Start()
    {
        SaveSystem.Initialize("results");
    }
    public void ScreenSwitching()
    {
        SceneManager.LoadScene("space_game");
    }
}
