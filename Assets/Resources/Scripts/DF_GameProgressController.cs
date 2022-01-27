using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DF_GameProgressController : MonoBehaviour
{
    private static int _numberOfPOIs;
    public static bool gameOver;

    private void OnEnable()
    {
        gameOver = false;
        _numberOfPOIs = 0;
    }

    public static int numberOfPOIs
    {
        get => _numberOfPOIs;
        set
        {
            if (value == _numberOfPOIs - 1)
            {
                _numberOfPOIs = value;
            } 
            else
            {
                _numberOfPOIs = value;
            }
        }
    }

    public static void EndGame()
    {
        Debug.Log("Ending game.");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
