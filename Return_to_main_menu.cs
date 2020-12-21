using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Return_to_main_menu : MonoBehaviour
{
    public void ScreenSwitching()
    {
        SceneManager.LoadScene("Start");
    }
}
