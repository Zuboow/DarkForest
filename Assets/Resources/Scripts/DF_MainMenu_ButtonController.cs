using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_MainMenu_ButtonController : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
